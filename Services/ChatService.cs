using ItiFinalProject.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Json;
using UglyToad.PdfPig;

namespace ItiFinalProject.Services
{
    public class ChatService : IChatService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        public ChatService(IConfiguration configuration, HttpClient client)
        {
            _configuration = configuration;
            _client = client;
            _client.Timeout = TimeSpan.FromSeconds(60);
        }

        public async Task<string> AddAsync(string pdfText, string Question)
        {
            if (string.IsNullOrWhiteSpace(Question))
            {
                return "Please write a question first";
            }

            string apiKey = _configuration["Gemini:ApiKey"]!;
            string primaryModel = _configuration["Gemini:ChatModel"] ?? "gemini-1.5-flash";

            // قائمة النماذج (الموديل الرئيسي من appsettings ثم نماذج احتياطية)
            string[] modelsToTry = new[] { primaryModel, "gemini-1.5-flash-8b", "gemini-1.5-pro" };

            string prompt = $"""
            you are a helpfull assistant.
            Answer The Question using only the Pdf Content Below.
            if you cant find the answer in the pdf ,
            say: "I Could not Find the answer in the Pdf File.
            Pdf Content:
            ---------------
            {pdfText}
            ---------------
            Question:
            {Question}
            """;

            var requestBody = new
            {
                contents = new[]
                {
                new
                {
                    parts = new[]
                    {
                        new { text = prompt }
                    }
                }
            }
            };

            string json = JsonSerializer.Serialize(requestBody);

            foreach (var model in modelsToTry)
            {
                string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={apiKey}";

                // 3 محاولات لكل موديل مع انتظار تصاعدي لتجاوز خطأ 503 High Demand
                for (int retry = 0; retry < 3; retry++)
                {
                    try
                    {
                        using var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = await _client.PostAsync(url, content);
                        string responseJson = await response.Content.ReadAsStringAsync();

                        if (response.IsSuccessStatusCode)
                        {
                            using JsonDocument document = JsonDocument.Parse(responseJson);
                            string answer = document.RootElement
                                .GetProperty("candidates")[0]
                                .GetProperty("content")
                                .GetProperty("parts")[0]
                                .GetProperty("text")
                                .GetString() ?? "";

                            return answer;
                        }

                        int statusCode = (int)response.StatusCode;

                        // إذا كان السيرفر مشغولاً (503) أو تم تجاوز Rate Limit (429)
                        if (statusCode == 503 || statusCode == 429)
                        {
                            await Task.Delay((retry + 1) * 2000); // انتظر 2s ثم 4s
                            continue;
                        }

                        // إذا كان الخطأ من نوع آخر، انتقل للموديل التالي مباشرة
                        break;
                    }
                    catch (HttpRequestException)
                    {
                        await Task.Delay((retry + 1) * 2000);
                    }
                    catch (Exception ex)
                    {
                        return $"An error occurred while processing the request: {ex.Message}";
                    }
                }
            }

            return "The service is currently busy; please try again in a few seconds";
        }
        //public async Task<string> AddAsync(string pdfText, string Question)
        //{
        //    var configuration = new ConfigurationBuilder()
        //        .AddJsonFile("appsettings.json").Build();

        //    string apiKey = configuration["Gemini:ApiKey"]!;
        //    string model = configuration["Gemini:ChatModel"]!;

        //    string prompt = $"""
        //        you are a helpfull assistant.
        //        Answer The Question using only the Pdf Content Below.
        //        if you cant find the answer in the pdf ,
        //        say: "I Could not Find the answer in the Pdf File.
        //        Pdf Content:
        //        ---------------
        //        {pdfText}
        //        ---------------
        //        Question:
        //        {Question}
        //        """;
        //    using HttpClient client = new HttpClient();
        //    client.Timeout = TimeSpan.FromSeconds(60);
        //    client.DefaultRequestHeaders.Add("x-goog-api-key", apiKey);
        //    var requestBody = new
        //    {
        //        contents = new[]
        //        {
        //            new
        //            {
        //                parts= new[]
        //                {
        //                    new
        //                    {
        //                        text=prompt
        //                    }
        //                }
        //            }
        //        }
        //    };

        //    string json = JsonSerializer.Serialize(requestBody);
        //    using var content = new StringContent(json, encoding: Encoding.UTF8, "application/json");
        //    var response = await client.PostAsync($"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent",content);

        //    string responseJson = await response.Content.ReadAsStringAsync();
        //    if(!response.IsSuccessStatusCode)
        //    {
        //        throw new Exception($"Gemini error {response.StatusCode}\n {responseJson}");
        //    }
        //    using JsonDocument document = JsonDocument.Parse(responseJson);
        //    string answer = document.RootElement
        //        .GetProperty("candidates")[0]
        //        .GetProperty("content")
        //        .GetProperty("parts")[0]
        //        .GetProperty("text")
        //        .GetString() ?? "";
        //    return answer;
        //}

        public string ReadPdf(IFormFile PdfFile)
        {
            using var stream = PdfFile.OpenReadStream();
            using var document = PdfDocument.Open(stream);
            string text = "";
            foreach (var page in document.GetPages())
            {
                text += page.Text + Environment.NewLine;
            }
            return text;
        }
    }
}

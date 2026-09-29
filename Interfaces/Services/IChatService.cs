namespace ItiFinalProject.Interfaces.Services
{
    public interface IChatService
    {
        string ReadPdf(IFormFile PdfFile);

        Task<string> AddAsync(string pdfText, string Question);
    }
}

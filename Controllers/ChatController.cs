using ItiFinalProject.Interfaces.Services;
using ItiFinalProject.View_Model.Chat_Bot;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Threading.Tasks;

namespace ItiFinalProject.Controllers
{
    public class ChatController : Controller
    {
        private readonly IChatService _chatService;
        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Authorize]
        public  async Task<IActionResult> Ask(ChatBotViewModel ChatVM)
        {
            if (ChatVM.PDfFile == null)
            {
                ChatVM.Answer = "Please Upload File";
                return View("Index", ChatVM);
            }
            if (string.IsNullOrWhiteSpace(ChatVM.Question))
            {
                ChatVM.Answer = "Please enter a question";
                return View("Index", ChatVM);
            }
            var PdfText = _chatService.ReadPdf(ChatVM.PDfFile);
            ChatVM.Answer = await _chatService.AddAsync(PdfText, ChatVM.Question);
            return View("Index",ChatVM);
        }
    }
}

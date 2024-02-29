using wealthpix.Models;
using wealthpix.Services;
using Microsoft.AspNetCore.Mvc;

namespace wealthpix.Controllers
{
    public class wealthpixChatController : Controller
    {
        private readonly IVertexAiService _palmService;

        public wealthpixChatController(IVertexAiService paLMService)
        {
            _palmService = paLMService;
        }

        [HttpGet]
        public async Task<IActionResult> Intro()
        {
            return await Chat("Who are you and what can you do?");
        }

        [HttpPost]
        public async Task<IActionResult> Chat(string prompt)
        {
            wealthpixChatViewModel model = await _palmService.PredictAsync(prompt);
            return View("wealthpixChat", model);
        }
    }
}
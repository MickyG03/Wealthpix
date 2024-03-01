using wealthpix.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace wealthpix.Controllers
{

    public class HomeController : Controller
    {

         private readonly IConfiguration _configuration;

         public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var botName = _configuration["AppConfig:BotConfig:BotName"];
            var slogan = _configuration["AppConfig:BotConfig:Slogan"];

            var model = new homeModel(botName,slogan);

            return View(model);

        }
    }
}
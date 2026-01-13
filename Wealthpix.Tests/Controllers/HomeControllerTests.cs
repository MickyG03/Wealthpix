using Microsoft.Extensions.Configuration;
using wealthpix.Controllers;
using wealthpix.Models;

namespace Wealthpix.Tests.Controllers
{
    public class HomeControllerTests
    {
        [Fact]
        public void Index_returns_view_with_home_model_from_config()
        {
            var settings = new Dictionary<string, string?>
            {
                ["AppConfig:BotConfig:BotName"] = "TestBot",
                ["AppConfig:BotConfig:Slogan"] = "Test Slogan"
            };
            IConfiguration config = new ConfigurationBuilder()
                .AddInMemoryCollection(settings)
                .Build();

            var controller = new HomeController(config);

            var result = controller.Index() as ViewResult;

            result.Should().NotBeNull();
            result!.Model.Should().BeOfType<homeModel>();
            var model = (homeModel)result.Model!;
            model.BotName.Should().Be("TestBot");
            model.Slogan.Should().Be("Test Slogan");
        }
    }
}

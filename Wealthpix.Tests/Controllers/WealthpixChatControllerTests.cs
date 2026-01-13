using wealthpix.Controllers;
using wealthpix.Models;
using wealthpix.Services;

namespace Wealthpix.Tests.Controllers
{
    public class WealthpixChatControllerTests
    {
        [Fact]
        public async Task Intro_calls_chat_with_default_prompt_and_returns_view()
        {
            var expectedPrompt = "Who are you and what can you do?";
            var model = new wealthpixChatViewModel("Bot", "Slogan", new List<ChatHistory>());

            var svc = new Mock<IVertexAiService>();
            svc.Setup(s => s.PredictAsync(expectedPrompt)).ReturnsAsync(model);

            var controller = new wealthpixChatController(svc.Object);

            var result = await controller.Intro() as ViewResult;

            svc.Verify(s => s.PredictAsync(expectedPrompt), Times.Once);
            result.Should().NotBeNull();
            result!.ViewName.Should().Be("wealthpixChat");
            result.Model.Should().BeSameAs(model);
        }

        [Fact]
        public async Task Chat_returns_view_with_model_from_service()
        {
            var prompt = "Hello bot";
            var model = new wealthpixChatViewModel("Bot", "Slogan", new List<ChatHistory>());

            var svc = new Mock<IVertexAiService>();
            svc.Setup(s => s.PredictAsync(prompt)).ReturnsAsync(model);

            var controller = new wealthpixChatController(svc.Object);

            var result = await controller.Chat(prompt) as ViewResult;

            svc.Verify(s => s.PredictAsync(prompt), Times.Once);
            result.Should().NotBeNull();
            result!.ViewName.Should().Be("wealthpixChat");
            result.Model.Should().BeSameAs(model);
        }
    }
}

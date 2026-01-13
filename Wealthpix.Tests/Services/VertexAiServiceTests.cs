using Google.Cloud.AIPlatform.V1;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using wealthpix.Config;
using wealthpix.Models;
using wealthpix.Services;
using ProtoValue = Google.Protobuf.WellKnownTypes.Value;

namespace Wealthpix.Tests.Services
{
    public class VertexAiServiceTests
    {
        private static IOptions<AppConfig> CreateConfig() =>
            Options.Create(new AppConfig
            {
                BotConfig = new BotConfig
                {
                    BotName = "Bot",
                    Slogan = "Slogan",
                    Context = "Ctx"
                },
                ParameterConfig = new ParameterConfig
                {
                    Temperature = 0.5,
                    MaxOutputTokens = 128,
                    TopP = 0.9,
                    TopK = 40
                },
                PaLMApiConfig = new PaLMApiConfig
                {
                    Project = "proj",
                    Location = "loc",
                    Publisher = "pub",
                    Model = "model",
                    RegionEndpoint = "us-endpoint"
                }
            });

        [Fact]
        public async Task PredictAsync_empty_prompt_returns_fallback_and_skips_prediction()
        {
            var predictionClient = new Mock<IPredictionClient>();
            var factory = new Mock<IPredictionClientFactory>();
            factory.Setup(f => f.Create("us-endpoint")).Returns(predictionClient.Object);

            var examples = new Mock<IExamplesProvider>(MockBehavior.Strict);

            var svc = new VertexAiService(CreateConfig(), factory.Object, examples.Object);

            var result = await svc.PredictAsync(string.Empty);

            predictionClient.VerifyNoOtherCalls();
            examples.VerifyNoOtherCalls();
            result.BotName.Should().Be("Bot");
            result.Slogan.Should().Be("Slogan");
            result.ChatHistory.Should().HaveCount(2);
            result.ChatHistory[0].Content.Should().Be(string.Empty);
            result.ChatHistory[1].Content.Should().Be("Seems like you skipped the prompt, is there anything I can assist you with?");
        }

        [Fact]
        public async Task PredictAsync_normal_prompt_calls_prediction_and_builds_history()
        {
            var prompt = "hello";
            var botReply = "hi there";
            var examplesPayload = new dynamic[] { new { sample = "example" } };

            var predictionClient = new Mock<IPredictionClient>();
            predictionClient
                .Setup(p => p.PredictAsync(
                    It.IsAny<EndpointName>(),
                    It.IsAny<IList<ProtoValue>>(),
                    It.IsAny<ProtoValue>()))
                .ReturnsAsync(CreatePredictResponse(botReply));

            var factory = new Mock<IPredictionClientFactory>();
            factory.Setup(f => f.Create("us-endpoint")).Returns(predictionClient.Object);

            var examples = new Mock<IExamplesProvider>();
            examples.Setup(e => e.GetExamples()).Returns(examplesPayload);

            var svc = new VertexAiService(CreateConfig(), factory.Object, examples.Object);

            var result = await svc.PredictAsync(prompt);

            factory.Verify(f => f.Create("us-endpoint"), Times.Once);
            examples.Verify(e => e.GetExamples(), Times.Once);
            predictionClient.Verify(p => p.PredictAsync(
                It.Is<EndpointName>(e =>
                    e.ToString() == "projects/proj/locations/loc/publishers/pub/models/model"),
                It.Is<IList<ProtoValue>>(instances => ContainsPrompt(instances, prompt)),
                It.IsAny<ProtoValue>()), Times.Once);

            result.ChatHistory.Should().HaveCount(2);
            result.ChatHistory[0].Author.Should().Be("user");
            result.ChatHistory[0].Content.Should().Be(prompt);
            result.ChatHistory[1].Author.Should().Be("bot");
            result.ChatHistory[1].Content.Should().Be(botReply);
        }

        [Fact]
        public async Task PredictAsync_repeated_intro_prompt_does_not_duplicate_history()
        {
            var predictionClient = new Mock<IPredictionClient>();
            predictionClient
                .Setup(p => p.PredictAsync(It.IsAny<EndpointName>(), It.IsAny<IList<ProtoValue>>(), It.IsAny<ProtoValue>()))
                .ReturnsAsync(CreatePredictResponse("first"));

            var factory = new Mock<IPredictionClientFactory>();
            factory.Setup(f => f.Create("us-endpoint")).Returns(predictionClient.Object);

            var examples = new Mock<IExamplesProvider>();
            examples.Setup(e => e.GetExamples()).Returns(Array.Empty<dynamic>());

            var svc = new VertexAiService(CreateConfig(), factory.Object, examples.Object);

            await svc.PredictAsync("hello");
            var result = await svc.PredictAsync("Who are you and what can you do?");

            predictionClient.Verify(p => p.PredictAsync(It.IsAny<EndpointName>(), It.IsAny<IList<ProtoValue>>(), It.IsAny<ProtoValue>()), Times.Once);
            result.ChatHistory.Should().HaveCount(2);
            result.ChatHistory[0].Content.Should().Be("hello");
            result.ChatHistory[1].Content.Should().Be("first");
        }

        private static PredictResponse CreatePredictResponse(string content)
        {
            var response = new PredictResponse();
            var candidate = new ProtoValue
            {
                StructValue = new Struct
                {
                    Fields =
                    {
                        ["content"] = ProtoValue.ForString(content)
                    }
                }
            };
            var prediction = new ProtoValue
            {
                StructValue = new Struct
                {
                    Fields =
                    {
                        ["candidates"] = new ProtoValue
                        {
                            ListValue = new ListValue
                            {
                                Values = { candidate }
                            }
                        }
                    }
                }
            };
            response.Predictions.Add(prediction);
            return response;
        }

        private static bool ContainsPrompt(IList<ProtoValue> instances, string prompt)
        {
            if (instances.Count != 1) return false;
            var instance = instances[0];
            if (instance.StructValue?.Fields.TryGetValue("messages", out var messagesValue) != true)
                return false;
            var messages = messagesValue.ListValue?.Values;
            if (messages is null || messages.Count != 1) return false;
            var messageStruct = messages[0].StructValue;
            if (messageStruct is null) return false;
            return messageStruct.Fields.TryGetValue("content", out var contentValue)
                && contentValue.StringValue == prompt;
        }
    }
}

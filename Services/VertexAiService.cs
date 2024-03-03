// Remember to add GOOGLE_APPLICATION_CREDENTIALS in launchSettings.json / launch.json file as env variable
using System.Globalization;
using System.Text.Json;
using wealthpix.Config;
using wealthpix.Models;
using Google.Cloud.AIPlatform.V1;
using Google.Protobuf.WellKnownTypes;
using Microsoft.Extensions.Options;
using Value = Google.Protobuf.WellKnownTypes.Value;
using System.Diagnostics;

namespace wealthpix.Services
{
    public class VertexAiService: IVertexAiService
    {
        private readonly AppConfig _appConfig;
        private readonly List<ChatHistory> _history = new List<ChatHistory>();

        public VertexAiService(IOptions<AppConfig> appConfigOptions)
        {
            _appConfig = appConfigOptions.Value;
        }

        public async Task<wealthpixChatViewModel> PredictAsync(string prompt)
        {
            Debug.WriteLine(prompt);
            Debug.WriteLine("here");
            if (prompt == "Who are you and what can you do?" && _history.Count == 2){
                return BuildChatMessage("123repeatedinitialprompt321",  null);
            }
            if(prompt=="" || prompt ==" " || prompt==null ){
                return BuildChatMessage("",  null);
            }
            PredictionServiceClientBuilder serviceClientBuilder = new PredictionServiceClientBuilder
            {
                Endpoint = _appConfig.PaLMApiConfig.RegionEndpoint
            };
            PredictionServiceClient predictionServiceClient = serviceClientBuilder.Build();
            EndpointName endpoint = EndpointName.FromProjectLocationPublisherModel(
                _appConfig.PaLMApiConfig.Project, 
                _appConfig.PaLMApiConfig.Location, 
                _appConfig.PaLMApiConfig.Publisher, 
                _appConfig.PaLMApiConfig.Model);

            List<Value> instances = GetInstances(prompt);
            Value parameters = GetParameters();

            PredictResponse response = await predictionServiceClient.PredictAsync(endpoint, instances, parameters);

            return BuildChatMessage(prompt, response);
        }

        private wealthpixChatViewModel BuildChatMessage(string prompt,
                PredictResponse predictResponse)
        {
            string botMessage;
            if (predictResponse != null && 
                predictResponse.Predictions.Count > 0) 
            {
                botMessage = predictResponse.Predictions[0]
                        .StructValue.Fields["candidates"]
                        .ListValue.Values[0]
                        .StructValue.Fields["content"]
                        .StringValue;
            }
            else if (prompt=="" && predictResponse==null){
                botMessage="Seems like you skipped the prompt, is there anything I can assist you with?";
            }
            else
            {
                botMessage = "The LLM did not provide a response";
            }

            if(prompt != "123repeatedinitialprompt321"){

                _history.Add(new ChatHistory("user", prompt));
                _history.Add(new ChatHistory("bot", botMessage));

            }

            return new wealthpixChatViewModel(
                _appConfig.BotConfig.BotName,
                _appConfig.BotConfig.Slogan, 
                _history);
        }

        private dynamic[] GetExamples()
        {
            string json = File.ReadAllText("examples.json");
            return JsonSerializer.Deserialize<dynamic[]>(json);
        }

        private List<Value> GetInstances(string prompt)
        {
            return new List<Value>
            {
                new() {
                    StructValue = Struct.Parser.ParseJson(
                        JsonSerializer.Serialize(new 
                        {   
                            context = _appConfig.BotConfig.Context,  
                            examples = GetExamples(),
                            messages = new [] { new 
                                { 
                                    author = "user",
                                    content = prompt
                                }
                            }
                        }))
                }
            };
        }

        private Value GetParameters()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");
            return new Value()
            {
                StructValue = Struct.Parser.ParseJson(JsonSerializer.Serialize(new 
                {   
                    temperature = _appConfig.ParameterConfig.Temperature,
                    maxOutputTokens = _appConfig.ParameterConfig.MaxOutputTokens,
                    topP = _appConfig.ParameterConfig.TopP,
                    topK = _appConfig.ParameterConfig.TopK
                }))
            };
        }
    }
}
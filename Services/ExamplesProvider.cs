namespace wealthpix.Services
{
    public interface IExamplesProvider
    {
        dynamic[] GetExamples();
    }

    public class FileExamplesProvider : IExamplesProvider
    {
        public dynamic[] GetExamples()
        {
            string json = File.ReadAllText("examples.json");
            return System.Text.Json.JsonSerializer.Deserialize<dynamic[]>(json) ?? Array.Empty<dynamic>();
        }
    }
}

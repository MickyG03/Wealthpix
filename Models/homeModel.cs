namespace wealthpix.Models{
    public class homeModel{
        public homeModel(string botName, string slogan)
        {
            BotName = botName;
            Slogan = slogan;
        }

        public string BotName { get; set; }
        public string Slogan { get; set; }

    }
}
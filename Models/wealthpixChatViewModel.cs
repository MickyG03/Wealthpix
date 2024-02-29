namespace wealthpix.Models{
    public class wealthpixChatViewModel{
        public wealthpixChatViewModel(string botName, string slogan, List<ChatHistory> chatHistory)
        {
            BotName = botName;
            Slogan = slogan;
            ChatHistory = chatHistory;
        }

        public string BotName { get; set; }
        public string Slogan { get; set; }
        public List<ChatHistory> ChatHistory { get; set; }
    }
}
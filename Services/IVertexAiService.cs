using wealthpix.Models;

namespace wealthpix.Services{
    public interface IVertexAiService{
        Task<wealthpixChatViewModel> PredictAsync(string prompt);
    }
}
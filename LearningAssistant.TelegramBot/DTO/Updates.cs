using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot.DTO
{
    public class Updates
    {
        [JsonProperty("result")]
        public Update[] UpdateArr { get; set; }
    }
}

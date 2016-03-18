using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot.DTO
{
    public class Chat
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}

using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot.DTO
{
    public class Update
    {
        [JsonProperty("update_id")]
        public int UpdateID { get; set; }
        [JsonProperty("message")]
        public Message Message { get; set; }
    }
}

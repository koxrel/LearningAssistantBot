using Newtonsoft.Json;

namespace BotInCloud.DTO
{
    public class Message
    {
        [JsonProperty("message_id")]
        public int MessageId { get; set; }
        [JsonProperty("from")]
        public User User { get; set; }
        [JsonProperty("chat")]
        public Chat Chat { get; set; }
        [JsonProperty("text")]
        public string Text { get; set; }

    }
}

using Newtonsoft.Json;

namespace BotInCloud.DTO
{
    public class Chat
    {
        [JsonProperty("id")]
        public int Id { get; set; }
    }
}

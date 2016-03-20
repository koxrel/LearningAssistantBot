using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot.DTO
{
    public class User
    {
        [JsonProperty("result")]
        public User UserContainer { get; set; }
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("first_name")]
        public string Name { get; set; }
        [JsonProperty("last_name")]
        public string Surname { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
    }
}

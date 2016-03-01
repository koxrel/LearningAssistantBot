using Newtonsoft.Json;

namespace BotInCloud.DTO
{
    public class User
    {
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

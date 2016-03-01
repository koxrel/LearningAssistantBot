using Newtonsoft.Json;

namespace BotInCloud.DTO
{
    public class Updates
    {
        [JsonProperty("result")]
        public Update[] UpdateArr { get; set; }
    }
}

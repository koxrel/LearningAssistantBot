using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BotInCloud.DTO;
using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot
{
    public class BotWebRequest
    {
        private const string Token = "*";
        private const string Keyboard = @"{""keyboard"":[[""/homework_ielts"",""/homework_infotech""],[""/deadlines""]],""resize_keyboard"":true}";

        private readonly HttpClient _client = new HttpClient();

        private int _lastUpdateId;
        private CancellationTokenSource _cts;

        private Updates GetUpdates()
        {
            var response = _client.GetAsync($"https://api.telegram.org/bot{Token}/getupdates?offset={_lastUpdateId}").Result;

            return JsonConvert.DeserializeObject<Updates>(response.Content.ReadAsStringAsync().Result);
        }
    }
}

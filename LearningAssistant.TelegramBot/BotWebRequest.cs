using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BotInCloud.DTO;
using LearningAssistant.Database;
using LearningAssistant.Database.DataAccessImplementations;
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

        private void SendMessages(IEnumerable<Update> updates)
        {
            string reply;
            foreach (var update in updates)
            {
                if (update.Message.Text.StartsWith("/start"))
                    reply = Replies.Start;
                else if (update.Message.Text.StartsWith("/homework_ie"))
                    reply = TextBuilder.Summarize(Factory.DataAccess.GetCurrentIeltsHometask());
                else if (update.Message.Text.StartsWith("/homework_inf"))
                    reply = TextBuilder.Summarize(Factory.DataAccess.GetCurrentInfoTechHometask());
                else if (update.Message.Text.StartsWith("/dead"))
                    reply = TextBuilder.Summarize(Factory.DataAccess.GetCurrentDeadlines());
                else
                    reply = Replies.IncorrectCommand;

                _client.GetAsync(
                        $"https://api.telegram.org/bot{Token}/sendmessage?chat_id={update.Message.Chat.Id}&text={reply}&reply_markup={Keyboard}");

                Factory.DataAccess.AddUser();

                Factory.DisposeDataAccess();
            }

            
        }
    }
}

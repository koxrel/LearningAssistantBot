using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
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
        private BotWebRequest()
        {
            byte[] bytesToken = Convert.FromBase64String(ConfigurationManager.AppSettings["encodedPassword"]);
            _token = Encoding.UTF8.GetString(ProtectedData.Unprotect(bytesToken, null, DataProtectionScope.CurrentUser));
        }

        private static BotWebRequest _bot;

        public BotWebRequest Bot => _bot ?? (_bot = new BotWebRequest());

        private readonly string _token;
        private const string Keyboard = @"{""keyboard"":[[""/homework_ielts"",""/homework_infotech""],[""/deadlines""]],""resize_keyboard"":true}";

        private readonly HttpClient _client = new HttpClient();

        private int _lastUpdateId;
        private CancellationTokenSource _cts;
        
        private async Task<Updates> GetUpdates()
        {
            var response = await _client.GetAsync($"https://api.telegram.org/bot{_token}/getupdates?offset={_lastUpdateId}");

            var result = await response.Content.ReadAsStringAsync();

            return await Task<Updates>.Factory.StartNew(() => JsonConvert.DeserializeObject<Updates>(result));
        }

        private void SendMessages(IEnumerable<Update> updates)
        {
            foreach (var update in updates)
            {
                string reply;
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
                        $"https://api.telegram.org/bot{_token}/sendmessage?chat_id={update.Message.Chat.Id}&text={reply}&reply_markup={Keyboard}");

                Factory.DataAccess.AddUser(new Database.Entities.User
                {
                    FullName = $"{update.Message.User.Name} {update.Message.User.Surname}",
                    ChatId = update.Message.Chat.Id
                });

                _lastUpdateId = update.UpdateID + 1;
            }
            Factory.DisposeDataAccess();
        }

        private async void Process(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
                SendMessages((await GetUpdates()).UpdateArr);
        }

        public void StartProcessing()
        {
            _cts = new CancellationTokenSource();
            Task.Run(() => Process(_cts.Token));
        }

        public void CancelProcessing()
        {
            _cts.Cancel();
        }
    }
}

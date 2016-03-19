using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LearningAssistant.Database;
using LearningAssistant.Database.Classes;
using LearningAssistant.TelegramBot.DTO;
using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot
{
    public class BotWebRequest
    {
        private BotWebRequest()
        {
            try
            {
                _token = ConfigurationManager.AppSettings["APIKey"];
            }
            catch (Exception)
            {
                OnError?.Invoke();
            }
        }

        private static BotWebRequest _bot;

        public static BotWebRequest Bot => _bot ?? (_bot = new BotWebRequest());

        public bool IsActive => _cts != null && !_cts.IsCancellationRequested;

        public static event Action OnError;

        private readonly string _token;

        private const string Keyboard =
            @"{""keyboard"":[[""/homework_ielts"",""/homework_infotech""],[""/deadlines""]],""resize_keyboard"":true}";

        private readonly HttpClient _client = new HttpClient();

        private int _lastUpdateId;
        private CancellationTokenSource _cts;

        private async Task<Updates> GetUpdates()
        {
            var response =
                await _client.GetAsync($"https://api.telegram.org/bot{_token}/getupdates?offset={_lastUpdateId}");

            if (!response.IsSuccessStatusCode)
                throw new Exception();

            var result = await response.Content.ReadAsStringAsync();

            return await Task<Updates>.Factory.StartNew(() => JsonConvert.DeserializeObject<Updates>(result));
        }

        private async Task SendMessages(IEnumerable<Update> updates)
        {
            foreach (var update in updates)
            {
                string reply;
                if (update.Message.Text.StartsWith("/start"))
                    reply = Replies.Start;
                else if (update.Message.Text.StartsWith("/homework_ie"))
                    reply = TextBuilder.Summarize(await Factory.DataAccess.GetCurrentIeltsHometask());
                else if (update.Message.Text.StartsWith("/homework_inf"))
                    reply = TextBuilder.Summarize(await Factory.DataAccess.GetCurrentInfoTechHometask());
                else if (update.Message.Text.StartsWith("/dead"))
                    reply = TextBuilder.Summarize(await Factory.DataAccess.GetCurrentDeadlines());
                else
                    reply = Replies.IncorrectCommand;

                await _client.GetAsync(
                    $"https://api.telegram.org/bot{_token}/sendmessage?chat_id={update.Message.Chat.Id}&text={reply}&reply_markup={Keyboard}");

                await Factory.DataAccess.AddUser(new Database.Entities.User
                {
                    FullName = $"{update.Message.User.Name} {update.Message.User.Surname}",
                    ChatId = update.Message.Chat.Id
                });
                _lastUpdateId = update.UpdateID + 1;
            }
        }

        private async void Process(CancellationToken ct)
        {
            try
            {
                while (!ct.IsCancellationRequested)
                {
                    ct.ThrowIfCancellationRequested();
                    var messages = (await GetUpdates()).UpdateArr;
                    await SendMessages(messages);
                }
            }
            catch (Exception)
            {
                OnError?.Invoke();
            }
        }

        public async void SendBulkMessage(string message)
        {
            foreach (var user in await Factory.DataAccess.GetUsers())
            {
                if (_cts.IsCancellationRequested) return;
                await _client.GetAsync(
                    $"https://api.telegram.org/bot{_token}/sendmessage?chat_id={user.ChatId}&text={message}&reply_markup={Keyboard}");
                await Task.Delay(300);
            }
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

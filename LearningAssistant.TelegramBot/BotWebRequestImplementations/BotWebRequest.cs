using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using LearningAssistant.Database.Classes;
using LearningAssistant.TelegramBot.Classes;
using LearningAssistant.TelegramBot.DTO;
using LearningAssistant.TelegramBot.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LearningAssistant.TelegramBot.BotWebRequestImplementations
{
    public class BotWebRequest : IBotWebRequest
    {
        public BotWebRequest()
        {
            try
            {
                _token = ConfigurationManager.AppSettings["APIKey"];
            }
            catch (ConfigurationErrorsException)
            {
                throw new ConfigurationErrorsException("Could not load API Key from App.config!");
            }
        }

        public bool IsActive => _cts != null && !_cts.IsCancellationRequested;

        public string BotName { get; private set; }
        public string BotUsername { get; private set; }

        public event Action OnError;

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
                throw new HttpRequestException();

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
                Factory.DisposeDataAccess();
            }
            catch (Exception)
            {
                Factory.DisposeDataAccess();
                OnError?.Invoke();
            }
        }

        public async void SendBulkMessage(string message)
        {
            try
            {
                foreach (var user in await Factory.DataAccess.GetUsers())
                {
                    if (_cts.IsCancellationRequested) return;
                    await _client.GetAsync(
                        $"https://api.telegram.org/bot{_token}/sendmessage?chat_id={user.ChatId}&text={message}&reply_markup={Keyboard}");
                    await Task.Delay(300);
                }
            }
            catch (Exception)
            {
                Factory.DisposeDataAccess();
                CancelProcessing();
                OnError?.Invoke();
            }
        }

        public async Task GetBotName()
        {
            var response = await _client.GetAsync($"https://api.telegram.org/bot{_token}/getme");

            var result = await response.Content.ReadAsStringAsync();

            var user = await Task<User>.Factory.StartNew(() => JsonConvert.DeserializeObject<User>(result));

            BotName = user.UserContainer.Name;
            BotUsername = user.UserContainer.Username;
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

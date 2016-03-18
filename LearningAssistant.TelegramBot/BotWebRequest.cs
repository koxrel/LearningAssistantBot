﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using LearningAssistant.Database;
using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.TelegramBot.DTO;
using Newtonsoft.Json;

namespace LearningAssistant.TelegramBot
{
    public class BotWebRequest
    {
        private BotWebRequest()
        {
            byte[] bytesToken = Convert.FromBase64String(ConfigurationManager.AppSettings[Environment.UserName]);
            _token = Encoding.UTF8.GetString(ProtectedData.Unprotect(bytesToken, null, DataProtectionScope.CurrentUser));
        }

        private static BotWebRequest _bot;

        public static BotWebRequest Bot => _bot ?? (_bot = new BotWebRequest());

        public bool IsActive => _cts != null && !_cts.IsCancellationRequested;

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

        private async void SendMessages(IEnumerable<Update> updates)
        {
            foreach (var update in updates)
            {
                using (IDataAccess da = new DataAccess())
                {
                    string reply;
                    if (update.Message.Text.StartsWith("/start"))
                        reply = Replies.Start;
                    else if (update.Message.Text.StartsWith("/homework_ie"))
                        reply = TextBuilder.Summarize(await da.GetCurrentIeltsHometask());
                    else if (update.Message.Text.StartsWith("/homework_inf"))
                        reply = TextBuilder.Summarize(await da.GetCurrentInfoTechHometask());
                    else if (update.Message.Text.StartsWith("/dead"))
                        reply = TextBuilder.Summarize(await da.GetCurrentDeadlines());
                    else
                        reply = Replies.IncorrectCommand;

                    await _client.GetAsync(
                        $"https://api.telegram.org/bot{_token}/sendmessage?chat_id={update.Message.Chat.Id}&text={reply}&reply_markup={Keyboard}");

                    da.AddUser(new Database.Entities.User
                    {
                        FullName = $"{update.Message.User.Name} {update.Message.User.Surname}",
                        ChatId = update.Message.Chat.Id
                    });
                }
                _lastUpdateId = update.UpdateID + 1;
            }
        }

        private async void Process(CancellationToken ct)
        {
            while (!ct.IsCancellationRequested)
            {
                var messages = (await GetUpdates()).UpdateArr;
                SendMessages(messages);
                await Task.Delay(1000);
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

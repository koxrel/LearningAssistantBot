using System;
using System.Threading.Tasks;

namespace LearningAssistant.TelegramBot.Interfaces
{
    public interface IBotWebRequest
    {
        bool IsActive { get; }

        event Action OnError;
        Task GetBotName();
        void CancelProcessing();
        void SendBulkMessage(string message);
        void StartProcessing();
    }
}
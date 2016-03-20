using System;

namespace LearningAssistant.TelegramBot.Interfaces
{
    public interface IBotWebRequest
    {
        bool IsActive { get; }

        event Action OnError;

        void CancelProcessing();
        void SendBulkMessage(string message);
        void StartProcessing();
    }
}
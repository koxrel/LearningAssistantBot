using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;

namespace LearningAssistant.TelegramBot.Classes
{
    public static class Factory
    {
        private static IDataAccess _dataAccess;

        public static IDataAccess DataAccess => _dataAccess ?? (_dataAccess = new DataAccess());

        public static void DisposeDataAccess()
        {
            if (_dataAccess == null) return;

            _dataAccess.Dispose();
            _dataAccess = null;
        }
    }
}

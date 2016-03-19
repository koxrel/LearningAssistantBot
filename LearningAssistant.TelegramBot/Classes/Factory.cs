using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;

namespace LearningAssistant.TelegramBot.Classes
{
    public static class Factory
    {
        static IDataAccess _dataAccess;

        public static IDataAccess DataAccess
        {
            get
            {
                _dataAccess?.Dispose();
                _dataAccess = new DataAccess();
                return _dataAccess;
            }
        }

        public static void DisposeDataAccess()
        {
            if (_dataAccess == null) return;

            _dataAccess.Dispose();
            _dataAccess = null;
        }
    }
}

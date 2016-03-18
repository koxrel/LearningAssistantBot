using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;

namespace LearningAssistant.TelegramBot
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

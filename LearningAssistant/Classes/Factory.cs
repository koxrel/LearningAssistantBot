using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Interfaces;
using LearningAssistant.INavigatorImplementations;
using LearningAssistant.TelegramBot.Interfaces;
using LearningAssistant.TelegramBot.BotWebRequestImplementations;

namespace LearningAssistant.Classes
{
    static class Factory
    {
        private static INavigator _nav;
        private static IBotWebRequest _bot;
        public static IBotWebRequest GetBot => _bot ?? (_bot = new BotWebRequest());
        public static IDataAccess GetDataAccess => new DataAccess();
        public static INavigator GetNavigator => _nav ?? (_nav= new Navigator());
    }
}

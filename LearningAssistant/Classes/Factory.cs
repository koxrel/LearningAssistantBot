using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;
using LearningAssistant.Interfaces;
using LearningAssistant.INavigatorImplementations;

namespace LearningAssistant.Classes
{
    static class Factory
    {
        public static IDataAccess GetDataAccess => new DataAccess();
        public static INavigator GetNavigator => new Navigator();
    }
}

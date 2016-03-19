using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.DataAccessImplementations;
using LearningAssistant.Database.Interfaces;

namespace LearningAssistant
{
    static class Factory
    {
        public static IDataAccess GetDataAccess => new DataAccess();
    }
}

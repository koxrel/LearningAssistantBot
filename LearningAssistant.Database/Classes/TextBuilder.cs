using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database
{
    //The class and all its methods are made static since they do not rely on state
    public static class TextBuilder
    {
        public static string Summarize(IEnumerable<Deadline> deadlines)
        {
            IEnumerable<Deadline> deadlinesEnumerated = deadlines?.ToArray();
            if (deadlinesEnumerated == null || !deadlinesEnumerated.Any())
                return Info.NotFound;

            var sb = new StringBuilder();
            sb.Append("Крайние сроки сдачи работ:\n");
            foreach (var deadline in deadlinesEnumerated)
            {
                sb.Append(deadline);
                sb.Append('\n');
            }
            return sb.ToString();
        }

        public static string Summarize(Hometask hometask)
        {
            return hometask?.ToString() ?? Info.NotFound;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database
{
    public class TextBuilder
    {
        public string SummarizeDeadlines(IEnumerable<Deadline> deadlines)
        {
            IEnumerable<Deadline> deadlinesEnumerated = deadlines as Deadline[] ?? deadlines.ToArray();

            if (!deadlinesEnumerated.Any())
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

        public string SummarizeHometask(Hometask hometask)
        {
            return hometask?.ToString() ?? Info.NotFound;
        }
    }
}

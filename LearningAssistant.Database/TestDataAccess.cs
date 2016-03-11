using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database
{
    public class TestDataAccess
    {
        public List<Hometask> Hometasks { get; set; }
        public List<Deadline> Deadlines { get; set; }
        private List<User> Users;
           
        public TestDataAccess()
        {
            Hometasks = new List<Hometask>
            {
                new Hometask
                {
                    Description = "Exercises 1, 2, 3",
                    DueDate = DateTime.Now.AddDays(-1),
                    Id = 1,
                    Subject = "IELTS"
                },
                new Hometask
                {
                    Description = "Exercises 5, 6, 7",
                    DueDate = DateTime.Now.AddDays(1),
                    Id = 2,
                    Subject = "Economics"
                },
                new Hometask
                {
                    Description = "Exercises 8, 9, 10",
                    DueDate = DateTime.Now.AddMinutes(10),
                    Id = 3,
                    Subject = "InfoTech"
                }
            };
            Deadlines = new List<Deadline>
            {
                new Deadline
                {
                    Description = "Complete the survey",
                    DueDate = DateTime.Now.AddDays(-1),
                    Id = 1,
                    Subject = "Statistics"
                },
                new Deadline
                {
                    Description = "Design a database",
                    DueDate = DateTime.Now.AddDays(1),
                    Id = 2,
                    Subject = "Data management"
                },
                new Deadline
                {
                    Description = "Essay #1",
                    DueDate = DateTime.Now.AddDays(-1),
                    Id = 1,
                    Subject = "Enterprise Architecture"
                }
            };
        }

        public string GetCurrentDeadlines()
        {
            var query = Deadlines
                .Where(a => a.DueDate >= DateTime.Now)
                .OrderBy(a => a.DueDate)
                .ToArray();

            if (query.Length == 0)
                return Info.NotFound;

            var sb = new StringBuilder();
            sb.Append("Крайние сроки сдачи работ:\n");
            foreach (var assign in query)
            {
                sb.Append(assign);
                sb.Append('\n');
            }

            return sb.ToString();
        }
        public IEnumerable<Deadline> GetDeadlines()
        {
            return Deadlines;
        }

        public string GetCurrentIeltsHometask()
        {
            return Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefault(h => h.Subject == "IELTS" && h.DueDate > DateTime.Now)
                ?.ToString() ?? Info.NotFound;
        }

        public string GetCurrentInfoTechHometask()
        {
            return Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefault(h => h.Subject == "InfoTech" && h.DueDate > DateTime.Now)
                ?.ToString() ?? Info.NotFound;
        }

        public IEnumerable<Hometask> GetHomeTasks()
        {
            return Hometasks;
        }

        public void RemoveOldRecords()
        {
            var oldDeadlines = Deadlines.Where(d => d.DueDate < DateTime.Now);
            foreach (var oldDeadline in oldDeadlines)
            {
                Deadlines.Remove(oldDeadline);
            }

            var oldHometasks = Hometasks.Where(d => d.DueDate < DateTime.Now);
            foreach (var oldHometask in oldHometasks)
            {
                Hometasks.Remove(oldHometask);
            }
        }

        public void Dispose()
        {
            
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database
{
    public class DataAccess : IDisposable
    {
        private readonly Context _db = new Context();

        public IEnumerable<Deadline> GetCurrentDeadlines()
        {
            return _db.Deadlines
                .Where(a => a.DueDate >= DateTime.Now)
                .OrderBy(a => a.DueDate)
                .ToArray();
        }

        public IEnumerable<Deadline> GetDeadlines()
        {
            return _db.Deadlines;
        }

        public string GetCurrentIeltsHometask()
        {
            return _db.Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefault(h => h.Subject == "IELTS" && h.DueDate > DateTime.Now)
                ?.ToString() ?? Info.NotFound;
        }

        public Hometask GetCurrentInfoTechHometask()
        {
            return _db.Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefault(h => h.Subject == "InfoTech" && h.DueDate > DateTime.Now)
        }

        public IEnumerable<Hometask> GetHomeTasks()
        {
            return _db.Hometasks;
        }

        public void RemoveOldRecords()
        {
            var oldDeadlines = _db.Deadlines.Where(d => d.DueDate < DateTime.Now);
            _db.Deadlines.RemoveRange(oldDeadlines);

            var oldHometasks = _db.Hometasks.Where(d => d.DueDate < DateTime.Now);
            _db.Hometasks.RemoveRange(oldHometasks);

            _db.SaveChanges();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}

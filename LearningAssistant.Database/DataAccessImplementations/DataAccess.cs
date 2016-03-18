using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using LearningAssistant.Database;
using LearningAssistant.Database.Entities;
using LearningAssistant.Database.Interfaces;

namespace LearningAssistant.Database.DataAccessImplementations
{
    public class DataAccess : IDataAccess
    {
        public event Action OnError;

        private readonly Context _db;

        public DataAccess()
        {
            _db = new Context();
        }

        public async Task<IEnumerable<Deadline>> GetCurrentDeadlines()
        {
            return await _db.Deadlines
                .Where(a => a.DueDate >= DateTime.Now)
                .OrderBy(a => a.DueDate)
                .ToArrayAsync();
        }

        public async Task<IEnumerable<Deadline>> GetDeadlines()
        {
            return await _db.Deadlines.ToArrayAsync();
        }

        public async Task<Hometask> GetCurrentIeltsHometask()
        {
            return await _db.Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefaultAsync(h => h.Subject == "IELTS" && h.DueDate > DateTime.Now);
        }

        public async Task<Hometask> GetCurrentInfoTechHometask()
        {
            return await _db.Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefaultAsync(h => h.Subject == "InfoTech" && h.DueDate > DateTime.Now);
        }

        public async Task<IEnumerable<Hometask>> GetHomeTasks()
        {
            return await _db.Hometasks.ToArrayAsync();
        }

        public async void AddHometask(Hometask hometask)
        {
            _db.Hometasks.Add(hometask);
            await _db.SaveChangesAsync();
        }

        public async void AddDeadline(Deadline deadline)
        {
            _db.Deadlines.Add(deadline);
            await _db.SaveChangesAsync();
        }

        public void AddHometask(string subject, string description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException();

            AddHometask(new Hometask
            {
                Subject = subject,
                Description = description,
                DueDate = dueDate
            });
        }

        public void AddDeadline(string subject, string description, DateTime dueDate)
        {
            if (string.IsNullOrWhiteSpace(subject) || string.IsNullOrWhiteSpace(description))
                throw new ArgumentNullException();

            AddDeadline(new Deadline
            {
                Subject = subject,
                Description = description,
                DueDate = dueDate
            });
        }

        public async void AddUser(User user)
        {
            _db.Users.AddOrUpdate(u => u.ChatId, user);
            await _db.SaveChangesAsync();
        }

        public void AddUser(string fullname, int chatId)
        {
            if (string.IsNullOrWhiteSpace(fullname))
                throw new ArgumentNullException();

            AddUser(new User
            {
                FullName = fullname,
                ChatId = chatId
            });
        }

        public async void RemoveOldRecords()
        {
            var oldDeadlines = _db.Deadlines.Where(d => d.DueDate < DateTime.Now);
            _db.Deadlines.RemoveRange(oldDeadlines);

            var oldHometasks = _db.Hometasks.Where(d => d.DueDate < DateTime.Now);
            _db.Hometasks.RemoveRange(oldHometasks);

            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}

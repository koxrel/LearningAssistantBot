using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using LearningAssistant.Database.Classes;
using LearningAssistant.Database.Entities;
using LearningAssistant.Database.Interfaces;

namespace LearningAssistant.Database.DataAccessImplementations
{
    public class DataAccess : IDisposable, IDataAccess
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

        public Hometask GetCurrentIeltsHometask()
        {
            return _db.Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefault(h => h.Subject == "IELTS" && h.DueDate > DateTime.Now);
        }

        public Hometask GetCurrentInfoTechHometask()
        {
            return _db.Hometasks
                .OrderBy(h => h.DueDate)
                .FirstOrDefault(h => h.Subject == "InfoTech" && h.DueDate > DateTime.Now);
        }

        public IEnumerable<Hometask> GetHomeTasks()
        {
            return _db.Hometasks;
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

        public async void AddUser(User user)
        {
            _db.Users.AddOrUpdate(u => u.ChatId, user);
            await _db.SaveChangesAsync();
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

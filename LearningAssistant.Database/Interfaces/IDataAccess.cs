using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database.Interfaces
{
    public interface IDataAccess: IDisposable
    {
        Task AddDeadline(Deadline deadline);
        Task AddHometask(Hometask hometask);
        Task AddUser(User user);
        Task<IEnumerable<Deadline>> GetCurrentDeadlines();
        Task<Hometask> GetCurrentIeltsHometask();
        Task<Hometask> GetCurrentInfoTechHometask();
        Task<IEnumerable<User>> GetUsers();
        Task<IEnumerable<Deadline>> GetDeadlines();
        Task<IEnumerable<Hometask>> GetHomeTasks();
        Task RemoveOldRecords();
        Task AddDeadline(string subject, string description, DateTime dueDate);
        Task AddUser(string fullname, int chatId);
        Task AddHometask(string subject, string description, DateTime dueDate);
        Task RemoveDeadline(Deadline deadline);
        Task RemoveUser(User user);
        Task RemoveHometask(Hometask hometask);
    }
}
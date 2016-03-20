using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;
using LearningAssistant.Database.EntitiesInterfaces;

namespace LearningAssistant.Database.Interfaces
{
    public interface IDataAccess: IDisposable
    {
        Task AddDeadline(Deadline deadline);
        Task AddHometask(Hometask hometask);
        Task AddUser(User user);
        Task<IEnumerable<IDeadline>> GetCurrentDeadlines();
        Task<IHometask> GetCurrentIeltsHometask();
        Task<IHometask> GetCurrentInfoTechHometask();
        Task<IEnumerable<IUser>> GetUsers();
        Task<IEnumerable<IDeadline>> GetDeadlines();
        Task<IEnumerable<IHometask>> GetHomeTasks();
        Task RemoveOldRecords();
        Task AddDeadline(string subject, string description, DateTime dueDate);
        Task AddUser(string fullname, int chatId);
        Task AddHometask(string subject, string description, DateTime dueDate);
        Task RemoveDeadline(IDeadline deadline);
        Task RemoveUser(IUser user);
        Task RemoveHometask(IHometask hometask);
    }
}
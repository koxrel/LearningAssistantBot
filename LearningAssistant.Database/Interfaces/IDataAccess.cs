using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database.Interfaces
{
    public interface IDataAccess: IDisposable
    {
        void AddDeadline(Deadline deadline);
        void AddHometask(Hometask hometask);
        void AddUser(User user);
        void Dispose();
        Task<IEnumerable<Deadline>> GetCurrentDeadlines();
        Task<Hometask> GetCurrentIeltsHometask();
        Task<Hometask> GetCurrentInfoTechHometask();
        Task<IEnumerable<Deadline>> GetDeadlines();
        Task<IEnumerable<Hometask>> GetHomeTasks();
        void RemoveOldRecords();
    }
}
using System.Collections.Generic;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database
{
    public interface IDataAccess
    {
        void AddDeadline(Deadline deadline);
        void AddHometask(Hometask hometask);
        void AddUser(User user);
        void Dispose();
        IEnumerable<Deadline> GetCurrentDeadlines();
        Hometask GetCurrentIeltsHometask();
        Hometask GetCurrentInfoTechHometask();
        IEnumerable<Deadline> GetDeadlines();
        IEnumerable<Hometask> GetHomeTasks();
        void RemoveOldRecords();
    }
}
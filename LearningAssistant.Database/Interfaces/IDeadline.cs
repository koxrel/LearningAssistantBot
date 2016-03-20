using System;

namespace LearningAssistant.Database.Entities
{
    public interface IDeadline
    {
        string Description { get; set; }
        DateTime DueDate { get; set; }
        int Id { get; set; }
        string Subject { get; set; }

        string ToString();
    }
}
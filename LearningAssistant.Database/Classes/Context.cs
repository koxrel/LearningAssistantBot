using System.Data.Entity;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database.Classes
{
    public class Context : DbContext
    {
        public Context() : base("BotDatabase") { }

        public DbSet<Deadline> Deadlines { get; set; }
        public DbSet<Hometask> Hometasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

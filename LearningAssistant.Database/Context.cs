using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database
{
    public class Context : DbContext
    {
        public Context() : base("BotDatabase") { }

        public DbSet<Deadline> Deadlines { get; set; }
        public DbSet<Hometask> Hometasks { get; set; }
        public DbSet<User> Users { get; set; }
    }
}

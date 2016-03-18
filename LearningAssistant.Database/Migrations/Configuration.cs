using System.Collections.Generic;
using LearningAssistant.Database;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            var hometasks = new Hometask[]
            {
                new Hometask
                {
                    Description = "Exercises 1, 2, 3",
                    DueDate = DateTime.Now.AddDays(-1),
                    Id = 1,
                    Subject = "IELTS"
                },
                new Hometask
                {
                    Description = "Exercises 5, 6, 7",
                    DueDate = DateTime.Now.AddDays(1),
                    Id = 2,
                    Subject = "Economics"
                },
                new Hometask
                {
                    Description = "Exercises 8, 9, 10",
                    DueDate = DateTime.Now.AddMinutes(10),
                    Id = 3,
                    Subject = "InfoTech"
                }
            };

            var deadlines = new Deadline[]
            {
                new Deadline
                {
                    Description = "Complete the survey",
                    DueDate = DateTime.Now.AddDays(-1),
                    Id = 1,
                    Subject = "Statistics"
                },
                new Deadline
                {
                    Description = "Design a database",
                    DueDate = DateTime.Now.AddDays(1),
                    Id = 2,
                    Subject = "Data management"
                },
                new Deadline
                {
                    Description = "Essay #1",
                    DueDate = DateTime.Now.AddDays(-1),
                    Id = 1,
                    Subject = "Enterprise Architecture"
                }
            };

            var users = new User[]
            {
                new User
                {
                    FullName = "Sergey Pavlov",
                    ChatId = 99999999
                },
                new User
                {
                    FullName = "Igor Tresoumov",
                    ChatId = 1
                }
            };

            context.Deadlines.AddOrUpdate(d => d.Description, deadlines[0], deadlines[1], deadlines[2]);
            context.Hometasks.AddOrUpdate(h => h.Description, hometasks);
            context.Users.AddOrUpdate(u => u.ChatId, users);
        }
    }
}
    

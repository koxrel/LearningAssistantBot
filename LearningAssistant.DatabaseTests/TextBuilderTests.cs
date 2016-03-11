using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningAssistant.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database.Tests
{
    [TestClass()]
    public class TextBuilderTests
    {
        [TestMethod()]
        public void SummarizeDeadlines_Should_Return_Not_Found_On_Empty_Sequence_Test()
        {
            var text = new TextBuilder();

            Assert.AreEqual("Не найдено", text.SummarizeDeadlines(new List<Deadline>()));
        }

        [TestMethod()]
        public void SummarizeDeadlines_Should_Return_Not_Found_On_Null_Test()
        {
            var text = new TextBuilder();

            Assert.AreEqual("Не найдено", text.SummarizeDeadlines(null));
        }

        [TestMethod()]
        public void SummarizeDeadlines_Test()
        {
            var text = new TextBuilder();

            var deadlines = new List<Deadline>
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
                }
            };

            string rightCompare = $"Крайние сроки сдачи работ:\n{deadlines[0]}\n{deadlines[1]}\n";

            Assert.AreEqual(rightCompare, text.SummarizeDeadlines(deadlines));
        }

        [TestMethod()]
        public void SummarizeHometask_Return_Not_Found_On_Null_Test()
        {
            var text = new TextBuilder();

            Assert.AreEqual("Не найдено", text.SummarizeHometask(null));
        }

        [TestMethod()]
        public void SummarizeHometask_Test()
        {
            var text = new TextBuilder();

            var hometask = new Hometask
            {
                Description = "Exercises 8, 9, 10",
                DueDate = DateTime.Now.AddMinutes(10),
                Id = 3,
                Subject = "InfoTech"
            };

            Assert.AreEqual(hometask.ToString(), text.SummarizeHometask(hometask));
        }
    }
}
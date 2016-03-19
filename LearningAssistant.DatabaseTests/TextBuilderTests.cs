using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using LearningAssistant.Database.Entities;

namespace LearningAssistant.Database.Tests
{
    [TestClass()]
    public class TextBuilderTests
    {
        [TestMethod()]
        public void SummarizeDeadlines_Should_Return_Not_Found_On_Empty_Sequence_Test()
        {
            Assert.AreEqual("Не найдено", TextBuilder.Summarize(new List<Deadline>()));
        }

        [TestMethod()]
        public void SummarizeDeadlines_Should_Return_Not_Found_On_Null_Test()
        {
            Assert.AreEqual("Не найдено", TextBuilder.Summarize((IEnumerable<Deadline>) null));
        }

        [TestMethod()]
        public void SummarizeDeadlines_Test()
        {
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

            Assert.AreEqual(rightCompare, TextBuilder.Summarize(deadlines));
        }

        [TestMethod()]
        public void SummarizeHometask_Should_Return_Not_Found_On_Null_Test()
        {
            Assert.AreEqual("Не найдено", TextBuilder.Summarize((Hometask) null));
        }

        [TestMethod()]
        public void SummarizeHometask_Test()
        {
            var hometask = new Hometask
            {
                Description = "Exercises 8, 9, 10",
                DueDate = DateTime.Now.AddMinutes(10),
                Id = 3,
                Subject = "InfoTech"
            };

            Assert.AreEqual(hometask.ToString(), TextBuilder.Summarize(hometask));
        }
    }
}
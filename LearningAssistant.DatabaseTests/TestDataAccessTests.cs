using Microsoft.VisualStudio.TestTools.UnitTesting;
using LearningAssistant.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningAssistant.Database.Tests
{
    [TestClass()]
    public class TestDataAccessTests
    {
        [TestMethod()]
        public void GetCurrentDeadlinesTest()
        {
            var da = new TestDataAccess();
            string s = da.GetCurrentDeadlines();
        }

        [TestMethod()]
        public void GetDeadlinesTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCurrentIeltsHometaskTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetCurrentInfoTechHometaskTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void GetHomeTasksTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void RemoveOldRecordsTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void DisposeTest()
        {
            Assert.Fail();
        }
    }
}
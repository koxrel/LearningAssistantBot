using System.Windows.Media;
using LearningAssistant.Converters;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LearningAssistantTests
{
    [TestClass()]
    public class TextToColourConverterTests
    {
        [TestMethod()]
        public void Convert_Should_Return_Black_On_Null_Value_Test()
        {
            var c = new TextToColourConverter();
            Assert.AreEqual((new SolidColorBrush(Colors.Black)).ToString(), c.Convert(null, null, null, null).ToString());
        }

        [TestMethod()]
        public void Convert_Should_Return_Red_On_Inactive_Value_Test()
        {
            var c = new TextToColourConverter();
            Assert.AreEqual((new SolidColorBrush(Colors.Red)).ToString(), c.Convert("inactive", null, null, null).ToString());
        }

        [TestMethod()]
        public void Convert_Should_Return_Green_On_Active_Value_Test()
        {
            var c = new TextToColourConverter();
            Assert.AreEqual((new SolidColorBrush(Colors.Lime)).ToString(), c.Convert("active", null, null, null).ToString());
        }

        [TestMethod()]
        public void Convert_Should_Return_Black_On_Anything_Besides_Active_And_Inactive_Value_Test()
        {
            var c = new TextToColourConverter();
            Assert.AreEqual((new SolidColorBrush(Colors.Black)).ToString(), c.Convert("something", null, null, null).ToString());
        }

    }
}
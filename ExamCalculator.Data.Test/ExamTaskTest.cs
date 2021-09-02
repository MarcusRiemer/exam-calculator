using NUnit.Framework;

namespace ExamCalculator.Data.Test
{
    [TestFixture]
    public class ExamTaskTest
    {
        [Test]
        public void NumberOnlyNumeric()
        {
            Assert.AreEqual(new ExamTask.TaskNumber(1, ""), ExamTask.DecodeTaskNumber("1"));
            Assert.AreEqual(new ExamTask.TaskNumber(2, ""), ExamTask.DecodeTaskNumber("2"));
            Assert.AreEqual(new ExamTask.TaskNumber(12, ""), ExamTask.DecodeTaskNumber("12"));
        }

        [Test]
        public void NumberMixed()
        {
            Assert.AreEqual(new ExamTask.TaskNumber(1, "a"), ExamTask.DecodeTaskNumber("1a"));
            Assert.AreEqual(new ExamTask.TaskNumber(2, "b"), ExamTask.DecodeTaskNumber("2b"));
        }
    }
}
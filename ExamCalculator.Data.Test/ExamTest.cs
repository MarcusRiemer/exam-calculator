using System.Collections.Generic;
using NUnit.Framework;

namespace ExamCalculator.Data.Test
{
    [TestFixture]
    public class ExamTest
    {
        [Test]
        public void NextNumber_Empty()
        {
            var emptyExam = new Exam {Tasks = new List<ExamTask>()};

            Assert.AreEqual(new ExamTask.TaskNumber(1, ""), emptyExam.NextNumber(TaskInsertionIncrement.Task));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "a"), emptyExam.NextNumber(TaskInsertionIncrement.TaskSubFirst));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "a"), emptyExam.NextNumber(TaskInsertionIncrement.SubTask));
        }

        [Test]
        public void NextNumber_1a()
        {
            var emptyExam = new Exam {Tasks = new List<ExamTask>(new[] {new ExamTask {Number = "1a"}})};

            Assert.AreEqual(new ExamTask.TaskNumber(2, ""), emptyExam.NextNumber(TaskInsertionIncrement.Task));
            Assert.AreEqual(new ExamTask.TaskNumber(2, "a"), emptyExam.NextNumber(TaskInsertionIncrement.TaskSubFirst));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "b"), emptyExam.NextNumber(TaskInsertionIncrement.SubTask));
        }

        [Test]
        public void NextNumber_1a_2a_Middle()
        {
            var exam = new Exam
                {Tasks = new List<ExamTask>(new[] {new ExamTask {Number = "1a"}, new ExamTask {Number = "2a"}})};

            Assert.AreEqual(new ExamTask.TaskNumber(2, ""), exam.NextNumber(TaskInsertionIncrement.Task, 0));
            Assert.AreEqual(new ExamTask.TaskNumber(2, "a"), exam.NextNumber(TaskInsertionIncrement.TaskSubFirst, 0));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "b"), exam.NextNumber(TaskInsertionIncrement.SubTask, 0));
        }

        [Test]
        public void NextNumber_1a_2a_End()
        {
            var exam = new Exam
                {Tasks = new List<ExamTask>(new[] {new ExamTask {Number = "1a"}, new ExamTask {Number = "2a"}})};

            Assert.AreEqual(new ExamTask.TaskNumber(3, ""), exam.NextNumber(TaskInsertionIncrement.Task, 1));
            Assert.AreEqual(new ExamTask.TaskNumber(3, "a"), exam.NextNumber(TaskInsertionIncrement.TaskSubFirst, 1));
            Assert.AreEqual(new ExamTask.TaskNumber(2, "b"), exam.NextNumber(TaskInsertionIncrement.SubTask, 1));
        }
    }
}
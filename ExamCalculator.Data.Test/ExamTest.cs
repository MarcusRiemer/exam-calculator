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
            var emptyExam = new Exam{ Tasks = new List<ExamTask>() };
            
            Assert.AreEqual(new ExamTask.TaskNumber(1, ""), emptyExam.NextNumber(Exam.TaskInsertionIncrement.Task));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "a"), emptyExam.NextNumber(Exam.TaskInsertionIncrement.TaskSubFirst));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "a"), emptyExam.NextNumber(Exam.TaskInsertionIncrement.SubTask));
        }
        
        [Test]
        public void NextNumber_1a()
        {
            var emptyExam = new Exam{ Tasks = new List<ExamTask>( new []{ new ExamTask { Number = "1a"}} ) };
            
            Assert.AreEqual(new ExamTask.TaskNumber(2, ""), emptyExam.NextNumber(Exam.TaskInsertionIncrement.Task));
            Assert.AreEqual(new ExamTask.TaskNumber(2, "a"), emptyExam.NextNumber(Exam.TaskInsertionIncrement.TaskSubFirst));
            Assert.AreEqual(new ExamTask.TaskNumber(1, "b"), emptyExam.NextNumber(Exam.TaskInsertionIncrement.SubTask));
        }
    }
}
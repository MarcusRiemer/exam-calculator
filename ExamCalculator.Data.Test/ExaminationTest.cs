using System;
using System.Collections.Generic;
using NUnit.Framework;

namespace ExamCalculator.Data.Test
{
    [TestFixture]
    public class ExaminationTest
    {
        [Test]
        public void Exam1a2a_PupilX1Y2()
        {
            var db = new TestDatabase();
            if (!db.Database.EnsureCreated()) Assert.Fail("Database not created");

            var exam = new Exam
            {
                ExamId = Guid.NewGuid(),
                Tasks = new List<ExamTask>(new[]
                {
                    new ExamTask {ExamTaskId = Guid.NewGuid(), Number = "1a"},
                    new ExamTask {ExamTaskId = Guid.NewGuid(), Number = "2a"}
                })
            };

            db.TestDataContext.Exams.Add(exam);

            var pupils = new[]
            {
                new Pupil {PupilId = Guid.NewGuid(), FirstName = "X", LastName = "1"},
                new Pupil {PupilId = Guid.NewGuid(), FirstName = "Y", LastName = "2"}
            };

            var group = new Group {GroupId = Guid.NewGuid(), Name = "Testgroup", Pupils = pupils};

            db.TestDataContext.Groups.AddRange(group);
            db.TestDataContext.SaveChanges();

            var examination = exam.CreateExamination(DateTime.Now, group);

            Assert.AreEqual(4, examination.Results.Count);
        }
    }
}
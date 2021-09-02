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
            if (!db.Database.EnsureCreated())
            {
                Assert.Fail("Database not created");
            }
            
            var exam = new Exam
                {Tasks = new List<ExamTask>(new[] {new ExamTask {Number = "1a"}, new ExamTask {Number = "2a"}})};

            db.TestDataContext.Exams.Add(exam);
            
            var pupils = new []
            {
                new Pupil {FirstName = "X", LastName = "1", PupilId = Guid.NewGuid()},
                new Pupil {FirstName = "Y", LastName = "2", PupilId = Guid.NewGuid()}
            };
            
            db.TestDataContext.Pupils.AddRange(pupils);
            db.TestDataContext.SaveChanges();

            var examination = exam.CreateExamination(DateTime.Now, pupils);
            
            Assert.AreEqual(4, examination.Results.Count);
        }
    }
}
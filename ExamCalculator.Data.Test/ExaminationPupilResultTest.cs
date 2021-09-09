using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography.X509Certificates;
using NUnit.Framework;

namespace ExamCalculator.Data.Test
{
    [TestFixture]
    public class ExaminationPupilResultTest
    {
        private static ExaminationPupilResult[] GenerateData(int[] scores, int[][] results)
        {
            using var db = new TestDatabase();
            Pupil[] pupils = results
                .Select((_, index) => new Pupil
                    {PupilId = Guid.NewGuid(), FirstName = 'A'.Next(index), LastName = '1'.Next(index)})
                .ToArray();
            
            db.TestDataContext.Pupils.AddRange(pupils);

            var group = new Group
            {
                GroupId = Guid.NewGuid(),
                Name = "7a",
                Pupils = pupils
            };
            
            db.TestDataContext.Groups.Add(group);

            var exam = new Exam
            {
                ExamId = Guid.NewGuid(),
                Name = "Cruel and Unusual Geography",
                Tasks = scores
                    .Select((maxScore, i) => new ExamTask
                        {ExamTaskId = Guid.NewGuid(), Number = '1'.Next(i), MaximumPoints = maxScore})
                    .ToArray()
            };
            
            db.TestDataContext.Exams.Add(exam);
            
            var examination = exam.CreateExamination(DateTime.Now, group);

            db.TestDataContext.Examinations.Add(examination);

            for (int pupilIndex = 0; pupilIndex < pupils.Length; pupilIndex++)
            {
                var pupil = pupils[pupilIndex];
                var pupilResults = examination.TaskResults.Where(r => r.PupilId == pupil.PupilId).ToArray();
                for (int taskIndex = 0; taskIndex < pupilResults.Count(); ++taskIndex)
                {
                    var taskResult = pupilResults[taskIndex];
                    taskResult.Score = results[pupilIndex][taskIndex];
                }
            }

            db.TestDataContext.SaveChanges();

            return pupils.Select(p => new ExaminationPupilResult(p, examination)).ToArray();
        }

        private static float SCORE_PERCENT_DELTA = 0.0001f;


        [Test]
        public void SinglePupilTwoTasksFull()
        {
            var data = GenerateData(new[] {2, 4}, new[] {new[] {2, 4}});
            var result = data[0];

            Assert.IsTrue(result.IsComplete);
            Assert.AreEqual(1, result.OverallPercentage, SCORE_PERCENT_DELTA);
        }
        
        [Test]
        public void SinglePupilTwoTasksHalf()
        {
            var data = GenerateData(new[] {2, 4}, new[] {new[] {1, 2}});
            var result = data[0];

            Assert.IsTrue(result.IsComplete);
            Assert.AreEqual(0.5f, result.OverallPercentage, SCORE_PERCENT_DELTA);
        }
        
        [Test]
        public void SinglePupilTwoTasksZero()
        {
            var data = GenerateData(new[] {2, 4}, new[] {new[] {0, 0}});
            var result = data[0];

            Assert.IsTrue(result.IsComplete);
            Assert.AreEqual(0, result.OverallPercentage, SCORE_PERCENT_DELTA);
        }
        
        [Test]
        public void ThreePupilsFullHalfZero()
        {
            var data = GenerateData(new[] {2, 4}, new[]
            {
                new[] {2, 4},
                new[] {1, 2},
                new[] {0, 0},
            });
            
            Assert.IsTrue(data.All(r => r.IsComplete));
            Assert.AreEqual(1.0f, data[0].OverallPercentage, SCORE_PERCENT_DELTA);
            Assert.AreEqual(0.5f, data[1].OverallPercentage, SCORE_PERCENT_DELTA);
            Assert.AreEqual(0.0f, data[2].OverallPercentage, SCORE_PERCENT_DELTA);
        }
    }
}
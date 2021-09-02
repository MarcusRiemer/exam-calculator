using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    /// <summary>
    ///     A specific exam that was taken by students
    /// </summary>
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExaminationId { get; set; }

        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }

        public ICollection<ExaminationTaskResult> Results { get; set; } = new List<ExaminationTaskResult>();

        public DateTime TakenOn { get; set; }

        public void AddPupil(Pupil p)
        {
            foreach (var task in Exam.Tasks)
                Results.Add(new ExaminationTaskResult
                    {Examination = this, Pupil = p, ExamTask = task});
        }
    }
}
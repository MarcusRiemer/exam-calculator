using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    /// <summary>
    /// A specific exam that was taken by students
    /// </summary>
    public class Examination
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExaminationId { get; set; }

        /// <summary>
        /// Foreign Key: Exam that is tested by this examination
        /// </summary>
        public Guid ExamId { get; set; }
        public Exam Exam { get; set; }

        /// <summary>
        /// Foreign Key: Group that is taking this exam
        /// </summary>
        public Guid GroupId { get; set; }

        public Group Group { get; set; }

        public ICollection<ExaminationTaskResult> TaskResults { get; set; } = new List<ExaminationTaskResult>();

        /// <summary>
        /// The time this exam was given to a certain group of pupils
        /// </summary>
        public DateTime TakenOn { get; set; }

        /// <summary>
        /// If a student takes an examination, he needs to solve every single task of the matching exam.
        /// </summary>
        /// <param name="p">The pupil taking the referenced Exam</param>
        public void PupilTakesExam(Pupil p)
        {
            foreach (var task in Exam.Tasks)
                TaskResults.Add(new ExaminationTaskResult
                    {Examination = this, Pupil = p, ExamTask = task});
        }
    }
}
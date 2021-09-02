using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    public class ExaminationTaskResult
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExaminationTaskResultId { get; set; }

        public Guid PupilId { get; set; }
        public Pupil Pupil { get; set; }

        public Guid ExaminationId { get; set; }
        public Examination Examination { get; set; }

        public Guid ExamTaskId { get; set; }
        public ExamTask ExamTask { get; set; }

        public int? Score { get; set; }
    }
}
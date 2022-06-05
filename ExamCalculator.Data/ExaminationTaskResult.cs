using System;
using System.Collections.Generic;
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

        public String[] Detail { get; } = {"1","2","3"};

        /// <summary>
        /// Percentage of points for this specific task. 0% if there is no score.
        /// </summary>
        public float Percent => ExamTask.MaximumPoints.HasValue ? (this.Score ?? 0) / (float)ExamTask.MaximumPoints.Value : 0;
    }
}
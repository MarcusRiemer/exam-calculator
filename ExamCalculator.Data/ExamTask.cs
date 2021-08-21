using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    [Table(nameof(ExamTask) + "s")]
    public class ExamTask
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExamTaskId { get; set; }

        public string Number { get; set; }
        
        public float MaximumPoints { get; set; }
        
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }
    }
}
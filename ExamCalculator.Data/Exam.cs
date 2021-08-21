using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    public class Exam
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExamId { get; set; }

        public string Name { get; set; }
        
        public ICollection<ExamTask> Tasks { get; set; }

    }
}
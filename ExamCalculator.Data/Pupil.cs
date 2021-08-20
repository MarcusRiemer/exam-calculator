using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    public class Pupil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PersonId { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    public class Pupil
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid PupilId { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string DisplayName => $"{LastName}, {FirstName}";

        public ICollection<Group> Groups { get; set; }
    }
}
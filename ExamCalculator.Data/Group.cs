using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamCalculator.Data
{
    public class Group
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GroupId { get; set; }

        public string Name { get; set; }

        public ICollection<Pupil> Pupils { get; set; }
    }
}
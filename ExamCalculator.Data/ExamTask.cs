using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic.CompilerServices;

namespace ExamCalculator.Data
{
    /// <summary>
    /// A single task that a pupil has to solve as part of an exam.
    /// </summary>
    [Table(nameof(ExamTask) + "s")]
    public class ExamTask
    {
        /// <summary>
        /// Valid numbers are defined by this regular expression
        /// </summary>
        public static readonly Regex NumberRegex = new("(?<Num>[0-9]+)(?<Task>[a-zA-Z]*)");

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExamTaskId { get; set; }

        /// <summary>
        /// Sortable representation of a number with tasks and sub tasks (1a, 2b) ...
        /// </summary>
        ///
        /// Yes, this sort of violates the first normal form, but it is properly sortable
        /// and on top of that quite flexibel if any teacher insists on strange ways to
        /// sort things.
        public string Number { get; set; }

        [NotMapped]
        public bool IsNumberValid => Number != null && NumberRegex.IsMatch(Number);

        /// <summary>
        /// How many points a pupil would retrieve if the question is answerred 100% correct.
        /// </summary>
        public float MaximumPoints { get; set; }

        /// <summary>
        /// The exam this task is part of.
        /// </summary>
        public Exam Exam { get; set; }
        public Guid ExamId { get; set; }

        /// <summary>
        /// Proper decomposition of a task number in the two parts of the hierarchy.
        /// </summary>
        public record TaskNumber(int num, string sub)
        {
            public string StringRepresentation => $"{num}{sub}";
        };

        public static TaskNumber DecodeTaskNumber(string number)
        {
            var matches = NumberRegex.Matches(number);
            var relevant = matches[0];

            return new TaskNumber(
                int.Parse(relevant.Groups["Num"].Value),
                relevant.Groups["Task"].Value
            );
        }
    }
}
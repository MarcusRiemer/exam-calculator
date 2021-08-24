using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ExamCalculator.Data
{
    /// <summary>
    /// A set of tasks that must be solved by students.
    /// </summary>
    public class Exam
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExamId { get; set; }

        /// <summary>
        /// An arbitrary name for the exam
        /// </summary>
        public string Name { get; set; }

        public ICollection<ExamTask> Tasks { get; set; }

        public enum TaskInsertionIncrement
        {
            /// <summary>
            /// Next number should increment the task number and have no subtask
            /// </summary>
            Task,
            /// <summary>
            /// Next number should be an incremented task number with the first subtask "a"
            /// </summary>
            TaskSubFirst,
            /// <summary>
            /// Next number should
            /// </summary>
            SubTask
        }

        /// <summary>
        /// Calculates the next free number for an assignment based on the existing numbers.
        /// </summary>
        /// <param name="inc">Mode to increment</param>
        /// <returns>The next free number</returns>
        public ExamTask.TaskNumber NextNumber(TaskInsertionIncrement inc)
        {
            var lastTask = Tasks.OrderBy(t => t.Number).LastOrDefault();
            // If there are no tasks at all the result must always be a first task
            // If the last task has no meaningful number: Just start new
            if (lastTask == null || string.IsNullOrEmpty(lastTask.Number))
            {
                return inc switch
                {
                    TaskInsertionIncrement.Task => new ExamTask.TaskNumber(1, ""),
                    _ => new ExamTask.TaskNumber(1, "a")
                };
            }
            else
            {
                
                var (num, sub) = ExamTask.DecodeTaskNumber(lastTask.Number);

                return inc switch
                {
                    TaskInsertionIncrement.Task => new ExamTask.TaskNumber(num + 1, ""),
                    TaskInsertionIncrement.TaskSubFirst => new ExamTask.TaskNumber(num + 1, "a"),
                    TaskInsertionIncrement.SubTask => new ExamTask.TaskNumber(num, IncrementLastChar(sub)),
                    _ => null
                };
            }
        }

        /// <summary>
        /// Increments a string like "1a" to "1b", not intended to use with other last characters than the alphabet.
        /// </summary>
        private static String IncrementLastChar(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "a";
            }
            else
            {
                Char last = str.Last();
                return str.Substring(0, str.Length - 1) + (++last);
            }
        }
    }
}
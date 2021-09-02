using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace ExamCalculator.Data
{
    /// <summary>
    ///     A set of tasks that must be solved by students. This should be seen as a template for examinations, because
    ///     the same exam may be presented to different groups of students.
    /// </summary>
    public class Exam
    {
        public const int LAST_TASK_INDEX = int.MaxValue;

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ExamId { get; set; }

        /// <summary>
        ///     An arbitrary name for the exam
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     The tasks that are to be solved as part of this exam.
        /// </summary>
        public ICollection<ExamTask> Tasks { get; set; } = new List<ExamTask>();

        /// <summary>
        ///     The attempts made by students to solve this exam.
        /// </summary>
        public ICollection<Examination> Examinations { get; set; } = new List<Examination>();

        public Examination CreateExamination(DateTime takenOn, IEnumerable<Pupil> pupils)
        {
            var examination = new Examination {Exam = this, TakenOn = takenOn, ExaminationId = Guid.NewGuid()};

            foreach (var pupil in pupils) examination.AddPupil(pupil);

            return examination;
        }

        /// <summary>
        ///     Calculates the next free number for an assignment based on the existing numbers.
        /// </summary>
        /// <param name="inc">Mode to increment</param>
        /// <param name="afterIndex">The index of the task to insert after</param>
        /// <returns>The next free number</returns>
        public ExamTask.TaskNumber NextNumber(TaskInsertionIncrement inc, int afterIndex = LAST_TASK_INDEX)
        {
            var tasks = Tasks.OrderBy(t => t.Number);
            var lastTask = afterIndex == LAST_TASK_INDEX
                ? tasks.LastOrDefault()
                : tasks.ElementAtOrDefault(afterIndex);

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

            var (num, sub) = ExamTask.DecodeTaskNumber(lastTask.Number);

            return inc switch
            {
                TaskInsertionIncrement.Task => new ExamTask.TaskNumber(num + 1, ""),
                TaskInsertionIncrement.TaskSubFirst => new ExamTask.TaskNumber(num + 1, "a"),
                TaskInsertionIncrement.SubTask => new ExamTask.TaskNumber(num, IncrementLastChar(sub)),
                _ => null
            };
        }

        /// <summary>
        ///     Increments a string like "1a" to "1b", not intended to use with other last characters than the alphabet.
        /// </summary>
        private static string IncrementLastChar(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "a";
            }

            var last = str.Last();
            return str.Substring(0, str.Length - 1) + ++last;
        }
    }

    public enum TaskInsertionIncrement
    {
        /// <summary>
        ///     Next number should increment the task number and have no subtask
        /// </summary>
        Task,

        /// <summary>
        ///     Next number should be an incremented task number with the first subtask "a"
        /// </summary>
        TaskSubFirst,

        /// <summary>
        ///     Next number should
        /// </summary>
        SubTask
    }
}
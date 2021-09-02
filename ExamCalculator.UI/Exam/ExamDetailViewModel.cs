using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Threading.Tasks;
using Avalonia.Controls;
using DynamicData;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExamDetailViewModel : ReactiveObject, IRoutableViewModel
    {
        public ExamDetailViewModel(IScreen screen, Guid examId)
        {
            HostScreen = screen;
            ExamTasks = new ObservableCollection<ExamTask>();

            ExamId = new BehaviorSubject<Guid>(examId);
            Exam = ExamId
                .Select(id => Database.Exams.Find(id));

            Caption = Exam.Select(e => e.Name);
            Exam.Select(curr =>
                Database.ExamTasks
                    .Where(et => et.ExamId == curr.ExamId)
                    .OrderBy(e => e.Number)
            ).Subscribe(res =>
            {
                ExamTasks.Clear();
                ExamTasks.AddRange(res);
            });

            CreateTask = ReactiveCommand.Create(
                async (TaskInsertionIncrement inc) =>
                {
                    Console.Out.WriteLine($"Selected Index: {NewTaskSelectedIndex}");
                    if (NewTaskSelectedIndex == -1)
                    {
                        NewTaskSelectedIndex = Data.Exam.LAST_TASK_INDEX;
                    }
                    
                    var exam = await Exam.FirstAsync();
                    var nextNumber = exam.NextNumber(inc, NewTaskSelectedIndex);
                    Database.ExamTasks.Add(new ExamTask
                    {
                        ExamId = exam.ExamId,
                        ExamTaskId = Guid.NewGuid(),
                        Number = nextNumber.StringRepresentation,
                        MaximumPoints = NewTaskCurrentPoints
                    });
                    Database.SaveChanges();

                    // Mark exam as changed
                    ExamId.OnNext(ExamId.Value);
                });
            
            RemoveTask = ReactiveCommand.Create(
                async (ExamTask task) =>
                {
                    var exam = await Exam.FirstAsync();
                    exam.Tasks.Remove(task);
                    
                    Database.SaveChanges();

                    // Mark exam as changed
                    ExamId.OnNext(ExamId.Value);
                });
        }
        
        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = ExamTasks.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }
        }

        public Subject<Unit> ExamTasksChanged = new();

        public BehaviorSubject<Guid> ExamId { get; }

        public IObservable<Exam> Exam { get; }

        public ObservableCollection<ExamTask> ExamTasks { get; }

        public IObservable<string> Caption { get; }

        public ReactiveCommand<TaskInsertionIncrement, Task> CreateTask { get; }
        
        public ReactiveCommand<ExamTask, Task> RemoveTask { get; }
        
        public int NewTaskSelectedIndex { get; set; }
        
        public int NewTaskCurrentPoints { get; set; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/exam/:id";
    }
}
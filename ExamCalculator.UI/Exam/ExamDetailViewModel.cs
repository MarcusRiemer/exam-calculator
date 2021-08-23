using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
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
                () =>
                {
                    Database.ExamTasks.Add(new ExamTask {ExamId = ExamId.Value, ExamTaskId = Guid.NewGuid()});
                    Database.SaveChanges();

                    // Mark exam as changed
                    ExamId.OnNext(ExamId.Value);
                });
        }

        public Subject<Unit> ExamTasksChanged = new();

        public BehaviorSubject<Guid> ExamId { get; }

        public IObservable<Exam> Exam { get; }

        public ObservableCollection<ExamTask> ExamTasks { get; }

        public IObservable<string> Caption { get; }

        public ReactiveCommand<Unit, Unit> CreateTask { get; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/exam/:id";
    }
}
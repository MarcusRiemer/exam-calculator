using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
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

            RefreshData(examId);

            CreateTask = ReactiveCommand.Create(
                () =>
                {
                    Database.ExamTasks.Add(new ExamTask {ExamId = Exam.ExamId, ExamTaskId = Guid.NewGuid()});
                    Database.SaveChanges();

                    RefreshData(examId);
                });
        }

        public Exam Exam { get; private set; }

        public ObservableCollection<ExamTask> ExamTasks { get; }

        public string Caption => Exam.Name;

        public ReactiveCommand<Unit, Unit> CreateTask { get; }

        private ApplicationDataContext Database { get; } = new();

        private void RefreshData(Guid examId)
        {
            Exam = Database.Exams.Find(examId);

            var tasks = Database.ExamTasks
                .Where(et => et.ExamId == examId)
                .OrderBy(e => e.Number);
            
            ExamTasks.Clear();
            ExamTasks.AddRange(tasks);
        }

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/exam/:id";
    }
}
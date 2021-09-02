using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ExamCalculator.Data;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExamOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public ExamOverviewViewModel(IScreen screen, RoutingState router)
        {
            HostScreen = screen;
            Exams = new ObservableCollection<Exam>(Database.Exams);

            Create = ReactiveCommand.Create(
                () =>
                {
                    var exam = Database.Exams.Add(
                        new Exam {ExamId = Guid.NewGuid()}
                    );
                    Database.SaveChanges();

                    Exams.Add(exam.Entity);
                });

            Delete = ReactiveCommand.Create(
                (Exam exam) =>
                {
                    Database.Exams.Remove(exam);
                    Database.SaveChanges();

                    Exams.Remove(exam);
                }
            );

            GoDetails = ReactiveCommand.CreateFromObservable(
                (Guid examId) => router.Navigate.Execute(new ExamDetailViewModel(screen, examId))
            );
        }

        public ObservableCollection<Exam> Exams { get; }

        public ReactiveCommand<Unit, Unit> Create { get; }

        public ReactiveCommand<Exam, Unit> Delete { get; }

        public ReactiveCommand<Guid, IRoutableViewModel> GoDetails { get; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/exam";

        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = Exams.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }
        }
    }
}
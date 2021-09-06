using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using ExamCalculator.Data;
using ExamCalculator.Service.UI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using ReactiveUI;
using Splat;

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
                async (Exam exam) =>
                {
                    var count = Database.Examinations.Count(ex => ex.ExamId == exam.ExamId);
                    var box = MessageBoxManager.GetMessageBoxStandardWindow(
                        "Vorsicht: Zu dieser Klausur existieren Ergebnisse!",
                        $"Wenn diese Klausur gelöscht wird, werden auch {count} Prüfungen gelöscht! Wirklich löschen?",
                        ButtonEnum.YesNo
                    );
                    var result = await DialogService.ShowDialog(box);
                    var doDelete = result == ButtonResult.Yes;
                    if (doDelete)
                    {
                        Database.Exams.Remove(exam);
                        Database.SaveChanges();

                        Exams.Remove(exam);
                    }
                }
            );

            GoDetails = ReactiveCommand.CreateFromObservable(
                (Guid examId) => router.Navigate.Execute(new ExamDetailViewModel(screen, router, examId))
            );
        }

        public ObservableCollection<Exam> Exams { get; }

        public ReactiveCommand<Unit, Unit> Create { get; }

        public ReactiveCommand<Exam, Task> Delete { get; }

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
        
        public IDialogService DialogService => (IDialogService) Locator.Current.GetService(typeof(IDialogService));
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public ExaminationOverviewViewModel(IScreen screen, RoutingState router)
        {
            HostScreen = screen;
            Examinations =
                new ObservableCollection<Examination>(Database.Examinations.Include(d => d.Exam).Include(d => d.Group));

            Delete = ReactiveCommand.Create(
                (Examination examination) =>
                {
                    Database.Examinations.Remove(examination);
                    Database.SaveChanges();

                    Examinations.Remove(examination);
                }
            );

            GoDetails = ReactiveCommand.CreateFromObservable(
                (Guid examinationId) => router.Navigate.Execute(new ExaminationDetailViewModel(screen, examinationId))
            );
        }

        public ObservableCollection<Examination> Examinations { get; }


        public ReactiveCommand<Examination, Unit> Delete { get; }

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
                var avaloniaInstance = Examinations.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }
        }
    }
}
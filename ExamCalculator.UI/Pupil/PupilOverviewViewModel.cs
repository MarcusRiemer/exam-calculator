using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;
using ExamCalculator.Data;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class PupilOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public PupilOverviewViewModel(IScreen screen)
        {
            HostScreen = screen;
            Pupils = new ObservableCollection<Pupil>(Database.Pupils);
        }

        private ApplicationDataContext Database { get; } = new();

        public ObservableCollection<Pupil> Pupils { get; }

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/pupil";

        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = Pupils.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Console.WriteLine($"Avalonia: {avaloniaInstance.FirstName}, DB: {dbInstance.Entity.FirstName}");
                Database.SaveChanges();
            }
        }
    }
}
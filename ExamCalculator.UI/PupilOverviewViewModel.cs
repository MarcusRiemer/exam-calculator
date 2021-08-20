using System;
using System.Collections.ObjectModel;
using ExamCalculator.Data;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class PupilOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public PupilOverviewViewModel(IScreen screen)
        {
            HostScreen = screen;
            using var data = new ApplicationDataContext();
            Pupils = new ObservableCollection<Pupil>(data.Pupils);
        }

        public ObservableCollection<Pupil> Pupils { get; }

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/pupil";
    }
}
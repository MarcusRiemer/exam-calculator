using ExamCalculator.Data;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExamOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public ExamOverviewViewModel(IScreen screen)
        {
            HostScreen = screen;
        }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/exam";
    }
}
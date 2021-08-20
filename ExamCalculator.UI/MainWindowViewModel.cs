using System.Reactive;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public MainWindowViewModel()
        {
            GoNext = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new FirstViewModel(this))
            );

            GoPupilOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new PupilOverviewViewModel(this))
            );
        }

        public ReactiveCommand<Unit, IRoutableViewModel> GoNext { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoPupilOverview { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        public string BindingProperty { get; } = "Test";

        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new();
    }
}
using System.Reactive;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class MainWindowViewModel : ReactiveObject, IScreen
    {
        public MainWindowViewModel()
        {
            GoPupilOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new PupilOverviewViewModel(this))
            );

            GoGroupOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new GroupOverviewViewModel(this, Router))
            );

            GoExamOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ExamOverviewViewModel(this, Router))
            );
            
            GoExaminationOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ExaminationOverviewViewModel(this, Router))
            );

            GoGroupOverview.Execute();
        }

        public ReactiveCommand<Unit, IRoutableViewModel> GoPupilOverview { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoGroupOverview { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoExamOverview { get; }
        
        public ReactiveCommand<Unit, IRoutableViewModel> GoExaminationOverview { get; }

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;

        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new();
    }
}
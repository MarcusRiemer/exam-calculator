using System;
using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData;
using ExamCalculator.Data;
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

            GoGroupDetail = ReactiveCommand.CreateFromObservable(
                (Guid groupId) => Router.Navigate.Execute(new GroupDetailViewModel(this, groupId))
            );

            GoExamOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ExamOverviewViewModel(this, Router))
            );
            
            GoExaminationOverview = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ExaminationOverviewViewModel(this, Router))
            );

            GoGroupOverview.Execute();

            SidebarGroups.AddRange(Database.Groups);
        }

        public ReactiveCommand<Unit, IRoutableViewModel> GoPupilOverview { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoGroupOverview { get; }
        
        public ReactiveCommand<Guid, IRoutableViewModel> GoGroupDetail { get; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoExamOverview { get; }
        
        public ReactiveCommand<Unit, IRoutableViewModel> GoExaminationOverview { get; }

        public ObservableCollection<Group> SidebarGroups { get; } = new();

        // The command that navigates a user back.
        public ReactiveCommand<Unit, Unit> GoBack => Router.NavigateBack;
        
        public bool IsNewNavigation { get; } = true;
        
        private ApplicationDataContext Database { get; } = new();

        // The Router associated with this Screen.
        // Required by the IScreen interface.
        public RoutingState Router { get; } = new();
    }
}
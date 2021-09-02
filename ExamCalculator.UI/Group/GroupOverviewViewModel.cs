using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using Avalonia.Controls;
using ExamCalculator.Data;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class GroupOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public GroupOverviewViewModel(IScreen screen, RoutingState router)
        {
            HostScreen = screen;

            Groups = new ObservableCollection<Group>(Database.Groups);
            Create = ReactiveCommand.Create(
                () =>
                {
                    var group = Database.Groups.Add(
                        new Group {GroupId = Guid.NewGuid()}
                    );
                    Database.SaveChanges();

                    Groups.Add(group.Entity);
                });

            Delete = ReactiveCommand.Create(
                (Group group) =>
                {
                    Database.Groups.Remove(group);
                    Database.SaveChanges();

                    Groups.Remove(group);
                }
            );

            GoDetails = ReactiveCommand.CreateFromObservable(
                (Guid groupId) => router.Navigate.Execute(new GroupDetailViewModel(screen, groupId))
            );
        }


        public ObservableCollection<Group> Groups { get; }

        public ReactiveCommand<Unit, Unit> Create { get; }

        public ReactiveCommand<Group, Unit> Delete { get; }

        public ReactiveCommand<Guid, IRoutableViewModel> GoDetails { get; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/group";

        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = Groups.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }
        }
    }
}
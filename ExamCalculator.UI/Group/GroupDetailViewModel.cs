using System;
using System.Collections.ObjectModel;
using System.Linq;
using DynamicData;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class GroupDetailViewModel : ReactiveObject, IRoutableViewModel
    {
        public GroupDetailViewModel(IScreen screen, Guid groupId)
        {
            HostScreen = screen;
            Group = Database.Groups
                .Include(g => g.Pupils)
                .First(g => g.GroupId == groupId);
            GroupPupils = new ObservableCollection<Pupil>(Group.Pupils);
            AvailablePupils = new ObservableCollection<Pupil>(Database.Pupils);
        }
        
        public void OnSearchTextChanged(string currentSearch)
        {
            var newPupils = SearchedResultSet(currentSearch);
            AvailablePupils.Clear();
            AvailablePupils.AddRange(newPupils);
        }

        private IQueryable<Pupil> SearchedResultSet(string currentSearch) => Database.Pupils.Where(p =>
            string.IsNullOrEmpty(currentSearch)
            || p.FirstName.ToLower().Contains(currentSearch.ToLower())
            || p.LastName.ToLower().Contains(currentSearch.ToLower())
        );

        public Group Group { get; }

        public ObservableCollection<Pupil> GroupPupils { get; }

        public ObservableCollection<Pupil> AvailablePupils { get; }

        public string Caption => Group.Name;

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/group";
    }
}
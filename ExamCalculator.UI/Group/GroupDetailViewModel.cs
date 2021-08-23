using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using DynamicData;
using ExamCalculator.Data;
using MessageBox.Avalonia.Enums;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class GroupDetailViewModel : ReactiveObject, IRoutableViewModel
    {
        public GroupDetailViewModel(IScreen screen, Guid groupId)
        {
            HostScreen = screen;
            GroupPupils = new ObservableCollection<Pupil>();
            AvailablePupils = new ObservableCollection<Pupil>();
            
            RefreshData(groupId, "");
        }

        private void RefreshData(Guid groupId, String currentSearch)
        {
            Group = Database.Groups
                .Include(g => g.Pupils)
                .First(g => g.GroupId == groupId);
            
            GroupPupils.Clear();
            GroupPupils.AddRange(Group.Pupils);
            
            // Students are available if they are not
            var groupPupilIds = GroupPupils.Select(p => p.PupilId).ToArray();
            AvailablePupils.Clear();
            AvailablePupils.AddRange(SearchedResultSet(currentSearch).Where(p => !groupPupilIds.Contains(p.PupilId)));
        }

        public void OnSearchTextChanged(string currentSearch)
        {
            RefreshData(Group.GroupId, currentSearch);
        }

        public async void OnSeachAccept(Window window)
        {
            var count = AvailablePupils.Count;
            bool doAdd = count == 1;
            if (count > 1)
            {
                var box = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                    "Auswahl nicht eindeutig",
                    $"Wirklich {count} Schüler:innen zu dieser Klasse hinzufügen?",
                    ButtonEnum.YesNo
                );
                var result = await box.ShowDialog(window);
                doAdd = result == ButtonResult.Yes;
            }
            else if (count == 0)
            {
                var box = MessageBox.Avalonia.MessageBoxManager.GetMessageBoxStandardWindow(
                    "Keine Schüler:innen ausgewählt",
                    "Sofern die Namen von Schüler:innen in der aktuellen Liste auftauchen, könnten diese der Klasse hinzugefügt werden",
                    ButtonEnum.Ok
                );
                await box.ShowDialog(window);
            }

            if (doAdd)
            {
                foreach (var p in AvailablePupils)
                {
                    Group.Pupils.Add(p);
                }

                Database.SaveChanges();
            }
        }

        private IQueryable<Pupil> SearchedResultSet(string currentSearch) => Database.Pupils.Where(p =>
            string.IsNullOrEmpty(currentSearch)
            || p.FirstName.ToLower().Contains(currentSearch.ToLower())
            || p.LastName.ToLower().Contains(currentSearch.ToLower())
        );

        public Group Group { get; private set; }

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
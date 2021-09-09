using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia.Controls;
using DynamicData;
using ExamCalculator.Data;
using ExamCalculator.Service.UI;
using MessageBox.Avalonia;
using MessageBox.Avalonia.Enums;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;
using Splat;

namespace ExamCalculator.UI
{
    public class GroupDetailViewModel : ReactiveObject, IRoutableViewModel
    {
        private string _pupilNameQuery = "";

        public GroupDetailViewModel(IScreen screen, Guid groupId)
        {
            HostScreen = screen;
            GroupPupils = new ObservableCollection<Pupil>();
            AvailablePupils = new ObservableCollection<Pupil>();

            Group = new BehaviorSubject<Group>(
                Database.Groups
                    .Include(g => g.Pupils)
                    .First(g => g.GroupId == groupId)
            );

            Caption = Group.Select(g => $"Klasse {g.Name}");

            Group.Subscribe(g =>
                {
                    GroupPupils.Clear();
                    GroupPupils.AddRange(g.Pupils);
                }
            );

            this.WhenAnyValue(vm => vm.PupilNameQuery)
                .CombineLatest(Group)
                .Subscribe(
                    t =>
                    {
                        var query = t.First.ToLower();
                        var group = t.Second;
                        var groupPupilIds = GroupPupils.Select(p => p.PupilId).ToArray();
                        var availablePupils = Database.Pupils
                            .Where(p =>
                                string.IsNullOrEmpty(query)
                                || p.FirstName.ToLower().Contains(query)
                                || p.LastName.ToLower().Contains(query)
                            ).Where(p => !groupPupilIds.Contains(p.PupilId));

                        AvailablePupils.Clear();
                        AvailablePupils.AddRange(availablePupils);
                    });

            RemoveStudent = ReactiveCommand.Create(
                (Pupil p) =>
                {
                    Group.Value.Pupils.Remove(p);
                    Database.SaveChanges();
                    Group.OnNext(Group.Value);
                }
            );
        }

        public string PupilNameQuery
        {
            get => _pupilNameQuery;
            set => this.RaiseAndSetIfChanged(ref _pupilNameQuery, value);
        }

        public BehaviorSubject<Group> Group { get; }

        public ObservableCollection<Pupil> GroupPupils { get; }

        public ObservableCollection<Pupil> AvailablePupils { get; }

        public IObservable<string> Caption { get; }

        public ReactiveCommand<Pupil, Unit> RemoveStudent { get; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/group";

        public async void OnSeachAccept()
        {
            var selectedPupils = SelectedPupils.Any()
                ? AvailablePupils.Where(p => SelectedPupils.Contains(p))
                : AvailablePupils;
            
            var count = selectedPupils.Count();
            var doAdd = count == 1;
            if (count > 1)
            {
                var box = MessageBoxManager.GetMessageBoxStandardWindow(
                    "Auswahl nicht eindeutig",
                    $"Wirklich {count} Schüler:innen zu dieser Klasse hinzufügen?",
                    ButtonEnum.YesNo
                );
                var result = await DialogService.ShowDialog(box);
                doAdd = result == ButtonResult.Yes;
            }
            else if (count == 0)
            {
                var box = MessageBoxManager.GetMessageBoxStandardWindow(
                    "Keine Schüler:innen ausgewählt",
                    "Sofern die Namen von Schüler:innen in der aktuellen Liste auftauchen, könnten diese der Klasse hinzugefügt werden"
                );
                await DialogService.ShowDialog(box);
            }

            if (doAdd)
            {
                foreach (var p in selectedPupils)
                {
                    Group.Value.Pupils.Add(p);
                }
                Database.SaveChanges();
                Group.OnNext(Group.Value);
                PupilNameQuery = "";
            }
        }
        
        public IList<Pupil> SelectedPupils { get; } = new List<Pupil>();

        public IDialogService DialogService => (IDialogService) Locator.Current.GetService(typeof(IDialogService));
    }
    
 
}
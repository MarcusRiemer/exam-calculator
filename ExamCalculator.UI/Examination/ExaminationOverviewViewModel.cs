using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia.Controls;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationOverviewViewModel : ReactiveObject, IRoutableViewModel
    {
        public record ArgsCreate(Group? Group, Exam? Exam)
        {
            public bool IsValid => Group != null && Exam != null;
        };
        
        public ExaminationOverviewViewModel(IScreen screen, RoutingState router)
        {
            
            HostScreen = screen;
            Examinations =
                new ObservableCollection<Examination>(Database.Examinations.Include(d => d.Exam).Include(d => d.Group));
            Groups = new ObservableCollection<Group>(Database.Groups);
            Exams = new ObservableCollection<Exam>(Database.Exams);

            CanCreate = _createArgs.Select(c => c.IsValid);
            Create = ReactiveCommand.Create(
                () =>
                {
                    var examination = Database.Examinations.Add(
                        new Examination {Group = CreateArgs.Group, Exam = CreateArgs.Exam}
                    );
                    Database.SaveChanges();
                    Examinations.Add(examination.Entity);
                });
            
            Delete = ReactiveCommand.Create(
                (Examination examination) =>
                {
                    Database.Examinations.Remove(examination);
                    Database.SaveChanges();

                    Examinations.Remove(examination);
                }
            );

            GoDetails = ReactiveCommand.CreateFromObservable(
                (Guid examinationId) => router.Navigate.Execute(new ExaminationDetailViewModel(screen, examinationId))
            );
        }

        public ObservableCollection<Examination> Examinations { get; }
        
        public ObservableCollection<Group> Groups { get; }
        
        public ObservableCollection<Exam> Exams { get; }

        private readonly BehaviorSubject<ArgsCreate> _createArgs = new(new (null, null));
        public ArgsCreate CreateArgs { get => _createArgs.Value; set => _createArgs.OnNext(value); }
        
        public IObservable<bool> CanCreate { get; }
        
        public ReactiveCommand<Unit, Unit> Create { get; }
        
        public ReactiveCommand<Examination, Unit> Delete { get; }

        public ReactiveCommand<Guid, IRoutableViewModel> GoDetails { get; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/exam";

        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = Examinations.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }
        }
    }
}
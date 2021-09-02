using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia.Controls;
using DynamicData;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationDetailViewModel : ReactiveObject, IRoutableViewModel
    {
        public ExaminationDetailViewModel(IScreen screen, Guid examinationId)
        {
            HostScreen = screen;

            ExaminationId = new BehaviorSubject<Guid>(examinationId);

            Examination = ExaminationId.Select(id =>
                Database.Examinations
                    .Where(ex => ex.ExaminationId == id)
                    .Include(ex => ex.Exam)
                    .Include(ex => ex.Results)
                    .Include(ex => ex.Group)
                    .First());

            Exam = Examination.Select(examination => examination.Exam);

            Group = Examination.Select(examination => examination.Group);

            Caption = Examination.CombineLatest(Exam, Group)
                .Select(t => $"Klausur \"{t.Second.Name}\" in Klasse {t.Third.Name} am {t.First.TakenOn}");

            ExaminationTaskResult = new ObservableCollection<ExaminationTaskResult>();
            ExaminationId.Select(
                    examinationId => Database.ExaminationTaskResults
                        .Where(res => res.ExaminationId == examinationId)
                        .Include(res => res.Pupil)
                        .Include(res => res.ExamTask)
                        .OrderBy(res => res.Pupil.FirstName)
                        .ThenBy(res => res.ExamTask.Number)
                )
                .Subscribe(
                    results =>
                    {
                        ExaminationTaskResult.Clear();
                        ExaminationTaskResult.AddRange(results);
                    });
        }

        public BehaviorSubject<Guid> ExaminationId { get; }

        public IObservable<Examination> Examination { get; }

        public IObservable<Exam> Exam { get; }

        public IObservable<Group> Group { get; }

        public IObservable<string> Caption { get; }

        public ObservableCollection<ExaminationTaskResult> ExaminationTaskResult { get; }

        private ApplicationDataContext Database { get; } = new();

        // Reference to IScreen that owns the routable view model.
        public IScreen HostScreen { get; }

        // Unique identifier for the routable view model.
        public string UrlPathSegment { get; } = "/examination/:id";

        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = ExaminationTaskResult.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }
        }
    }
}
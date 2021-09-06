using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public abstract class ExaminationBaseViewModel : ReactiveObject, IRoutableViewModel
    {
        public ExaminationBaseViewModel(IScreen parentScreen, Guid examinationId)
        {
            HostScreen = parentScreen;

            ExaminationId = new BehaviorSubject<Guid>(examinationId);

            Examination = ExaminationId.Select(id =>
                Database.Examinations
                    .Where(ex => ex.ExaminationId == id)
                    .Include(ex => ex.Exam)
                    .Include(ex => ex.TaskResults)
                    .Include(ex => ex.Group)
                    .First());

            Exam = Examination.Select(examination => examination.Exam);

            Group = Examination.Select(examination => examination.Group);

            Caption = Examination.CombineLatest(Exam, Group)
                .Select(t => $"Klausur \"{t.Second.Name}\" in Klasse {t.Third.Name} am {t.First.TakenOn}");
        }
        
        public BehaviorSubject<Guid> ExaminationId { get; }

        public IObservable<Examination> Examination { get; }

        public IObservable<Exam> Exam { get; }

        public IObservable<Group> Group { get; }

        public IObservable<string> Caption { get; }

        protected ApplicationDataContext Database { get; } = new();
        
        public abstract string? UrlPathSegment { get; }
        
        public IScreen HostScreen { get; }
    }
}
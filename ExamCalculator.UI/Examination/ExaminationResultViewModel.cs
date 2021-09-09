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
    public class ExaminationResultViewModel  : ExaminationBaseViewModel
    {
        public ExaminationResultViewModel(IScreen parentScreen, Guid examinationId) : base(parentScreen, examinationId)
        {
            ExaminationId.Select(
                    examinationId => Database.Examinations
                        .Include(ex => ex.Group).ThenInclude(g => g.Pupils)
                        .Include(ex => ex.TaskResults).ThenInclude(ex => ex.ExamTask)
                        .First(ex => ex.ExaminationId == examinationId)
                )
                .Subscribe(
                    results =>
                    {
                        ExaminationPupilResult.Clear();
                        ExaminationPupilResult.AddRange(results.Group.Pupils
                            .OrderBy(p => p.FirstName)
                            .Select(p => new ExaminationPupilResult(p, results)));
                    });
        }

        public ObservableCollection<ExaminationPupilResult> ExaminationPupilResult { get; } = new();
        
        // Unique identifier for the routable view model.
        public override string UrlPathSegment { get; } = "/examination/result/:id";

        public void OnRowEditEnded(DataGridRowEditEndedEventArgs e)
        {
            /*if (e.EditAction == DataGridEditAction.Commit)
            {
                var avaloniaInstance = ExaminationTaskResult.ElementAt(e.Row.GetIndex());
                var dbInstance = Database.Entry(avaloniaInstance);
                dbInstance.CurrentValues.SetValues(avaloniaInstance);
                Database.SaveChanges();
            }*/
        }
    }
}
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Linq;
using Avalonia.Controls;
using DynamicData;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationScoreViewModel  : ExaminationBaseViewModel
    {
        public ExaminationScoreViewModel(IScreen parentScreen, Guid examinationId) : base(parentScreen, examinationId)
        {
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
        
        public ObservableCollection<ExaminationTaskResult> ExaminationTaskResult { get; }
        
        // Unique identifier for the routable view model.
        public override string UrlPathSegment { get; } = "/examination/score/:id";

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
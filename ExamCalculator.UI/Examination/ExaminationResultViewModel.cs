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
            
        }
        
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
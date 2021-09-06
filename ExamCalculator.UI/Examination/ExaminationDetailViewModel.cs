using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using Avalonia.Controls;
using DynamicData;
using ExamCalculator.Data;
using Microsoft.EntityFrameworkCore;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationDetailViewModel : ExaminationBaseViewModel, IScreen
    {
        public ExaminationDetailViewModel(IScreen parentScreen, Guid examinationId) : base(parentScreen, examinationId)
        {
            GoPoints = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ExaminationScoreViewModel(this, examinationId))
            );
            
            GoResult = ReactiveCommand.CreateFromObservable(
                () => Router.Navigate.Execute(new ExaminationResultViewModel(this, examinationId))
            );

            GoPoints.Execute();
        }

        public ReactiveCommand<Unit, IRoutableViewModel> GoResult { get; set; }

        public ReactiveCommand<Unit, IRoutableViewModel> GoPoints { get; }
        
        public RoutingState Router { get; } = new();
        
        // Unique identifier for the routable view model.
        public override string UrlPathSegment { get; } = "/examination/:id";
    }
}
using System;
using System.Reactive;
using ReactiveUI;

namespace ExamCalculator.UI
{
    /// <summary>
    /// Hosts detail information about an examination.
    /// </summary>
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
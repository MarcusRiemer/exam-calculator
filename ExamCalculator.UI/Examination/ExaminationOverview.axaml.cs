using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ExamCalculator.Data;

namespace ExamCalculator.UI
{
    public class ExaminationOverview : ReactiveUserControl<ExaminationOverviewViewModel>
    {
        public ExaminationOverview()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
        {
            ViewModel!.OnRowEditEnded(e);
        }

        private void OnGroupChanged(object? sender, SelectionChangedEventArgs e)
        {
            var prevArgs = ViewModel!.CreateArgs;
            var newGroup = e.AddedItems[0] as Group;
            ViewModel.CreateArgs = prevArgs with {Group = newGroup};
        }

        private void OnExamChanged(object? sender, SelectionChangedEventArgs e)
        {
            var prevArgs = ViewModel!.CreateArgs;
            var newExam = e.AddedItems[0] as Exam;
            ViewModel.CreateArgs = prevArgs with {Exam = newExam};
        }
    }
}
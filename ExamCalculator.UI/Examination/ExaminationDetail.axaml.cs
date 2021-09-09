using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationDetail : ReactiveUserControl<ExaminationDetailViewModel>
    {
        public ExaminationDetail()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnNavigatePoints(object? sender, RoutedEventArgs e)
        {
            ViewModel!.GoPoints.Execute();
        }

        private void OnNavigateResult(object? sender, RoutedEventArgs e)
        {
            ViewModel!.GoResult.Execute();
        }
    }
}
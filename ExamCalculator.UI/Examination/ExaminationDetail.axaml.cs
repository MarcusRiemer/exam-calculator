using Avalonia.Controls;
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

        private void OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
        {
            ViewModel.OnRowEditEnded(e);
        }
    }
}
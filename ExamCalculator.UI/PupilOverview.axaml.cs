using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ExamCalculator.Data;

namespace ExamCalculator.UI
{
    public class PupilOverview : ReactiveUserControl<PupilOverviewViewModel>
    {
        public PupilOverview()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
        
        private void OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
        {
            ViewModel.OnRowEditEnded(sender, e);
        }
    }
}
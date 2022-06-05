using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExaminationScore : ReactiveUserControl<ExaminationScoreViewModel>
    {
        public ExaminationScore()
        {
            AvaloniaXamlLoader.Load(this);
            
            var dataGrid = this.FindControl<DataGrid>("ScoreGrid");
            dataGrid.Columns.Add(new DataGridTextColumn()
            {
                Header = "Test",
                Binding = new Binding($"Detail[0]")
            });
        }

        private void OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
        {
            ViewModel!.OnRowEditEnded(e);
        }
    }
}
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExamOverview : ReactiveUserControl<ExamOverviewViewModel>
    {
        public ExamOverview()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
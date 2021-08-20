using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using ExamCalculator.Data;
using ReactiveUI;

namespace ExamCalculator.UI
{
    public class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            this.WhenActivated(disposables =>
            {
                ApplicationDataContext.EnsureDatabase();

            });
            AvaloniaXamlLoader.Load(this);
        }
    }
}
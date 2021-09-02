using Avalonia;
using Avalonia.Interactivity;
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
            this.WhenActivated(disposables => { ApplicationDataContext.EnsureDatabase(); });
            AvaloniaXamlLoader.Load(this);

#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void OnNavigatePupils(object? sender, RoutedEventArgs e)
        {
            ViewModel.GoPupilOverview.Execute();
        }

        private void OnNavigateExams(object? sender, RoutedEventArgs e)
        {
            ViewModel.GoExamOverview.Execute();
        }

        private void OnNavigateGroups(object? sender, RoutedEventArgs e)
        {
            ViewModel.GoGroupOverview.Execute();
        }

        private void OnNavigateExaminations(object? sender, RoutedEventArgs e)
        {
            ViewModel.GoExaminationOverview.Execute();
        }
    }
}
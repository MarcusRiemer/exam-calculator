using System;
using System.Linq;
using System.Reactive;
using Avalonia;
using Avalonia.Controls;
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


        private void OnNavigateGroupDetail(object? sender, RoutedEventArgs e)
        {
            var group = ((sender as TextBlock).DataContext as Group);
            ViewModel.GoGroupDetail.Execute(group.GroupId);
            e.Handled = true;
        }

        private void OnTreeItemSelected(object? sender, SelectionChangedEventArgs e)
        {
            Object senderData = null;
            if (e.AddedItems.Count == 1)
            {
                senderData = e.AddedItems[0];
            }
            
            switch (senderData)
            {
                case Group g: 
                    ViewModel.GoGroupDetail.Execute(g.GroupId);
                    break;
                case TreeViewItem t:
                    if (t.DataContext is ReactiveCommand<Unit, IRoutableViewModel> c)
                        c.Execute();
                    break;
                
            }
        }
    }
}
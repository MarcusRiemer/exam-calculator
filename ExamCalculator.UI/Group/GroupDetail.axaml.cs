using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;

namespace ExamCalculator.UI
{
    public class GroupDetail : ReactiveUserControl<GroupDetailViewModel>
    {
        public GroupDetail()
        {
            AvaloniaXamlLoader.Load(this);
            //var searchTerm = this.WhenAnyValue(x => x.SearchItem);
        }

        private void OnSearchKey(object? sender, KeyEventArgs e)
        {
            var t = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                ViewModel.OnSeachAccept((Window)this.GetVisualRoot());
                e.Handled = true;
            }
        }

        private void OnClickAddPupil(object? sender, RoutedEventArgs e)
        {
            ViewModel.OnSeachAccept((Window)this.GetVisualRoot());
        }
    }
}
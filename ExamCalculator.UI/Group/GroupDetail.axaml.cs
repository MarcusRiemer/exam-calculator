using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using Avalonia.VisualTree;
using DynamicData;
using ExamCalculator.Data;

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
                ViewModel!.OnSeachAccept();
                e.Handled = true;
            }
        }

        private void OnClickAddPupil(object? sender, RoutedEventArgs e)
        {
            ViewModel!.OnSeachAccept();
        }

        private void OnDataGridSelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            DataGrid dg = (DataGrid) sender!;
            ViewModel!.SelectedPupils.Clear();
            foreach (var selectedItem in dg.SelectedItems)
            {
                ViewModel.SelectedPupils.Add((Pupil)selectedItem);
            }
        }
    }
}
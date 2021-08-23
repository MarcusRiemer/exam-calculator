using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
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
        }

        private void OnSearchTextChanged(object? sender, TextInputEventArgs e)
        {
            throw new Exception("Previously, this never fired");
            ViewModel.OnSearchTextChanged(e.Text);
        }

        private void OnSearchKey(object? sender, KeyEventArgs e)
        {
            var t = sender as TextBox;
            if (e.Key == Key.Enter)
            {
                
                ViewModel.OnSeachAccept((Window)this.GetVisualRoot());
            }
            else
            {
                ViewModel.OnSearchTextChanged(t.Text);   
            }
        }
    }
}
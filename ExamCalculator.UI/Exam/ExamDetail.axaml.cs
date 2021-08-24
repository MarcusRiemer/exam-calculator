using System;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;

namespace ExamCalculator.UI
{
    public class ExamDetail : ReactiveUserControl<ExamDetailViewModel>
    {
        public ExamDetail()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void OnRowEditEnded(object? sender, DataGridRowEditEndedEventArgs e)
        {
            ViewModel.OnRowEditEnded(e);
        }
    }
}
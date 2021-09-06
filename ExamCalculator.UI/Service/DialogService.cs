using System.Threading.Tasks;
using Avalonia.Controls;
using MessageBox.Avalonia.BaseWindows.Base;

namespace ExamCalculator.Service.UI
{
    public interface IDialogService
    {
        public Task<T> ShowDialog<T>(IMsBoxWindow<T> dlg);
    }
    
    public class DialogService : IDialogService
    {
        public DialogService(Window window)
        {
            this.window = window;
        }


        public Task<T> ShowDialog<T>(IMsBoxWindow<T> dlg)
        {
            return dlg.ShowDialog(window);
        }

        private Window window;
    }
}
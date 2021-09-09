using System.Threading.Tasks;
using Avalonia.Controls;
using MessageBox.Avalonia.BaseWindows.Base;

namespace ExamCalculator.Service.UI
{
    public interface IDialogService
    {
        public Task<T> ShowDialog<T>(IMsBoxWindow<T> dlg);
    }
    
    /// <summary>
    /// Dialogs require a top level window that they need to block. This service
    /// holds on to such a window and makes it implicitly available.
    /// </summary>
    public class DialogService : IDialogService
    {
        public DialogService(Window window)
        {
            _window = window;
        }


        /// <summary>
        /// Shows a dialog that blocks the main window
        /// </summary>
        public Task<T> ShowDialog<T>(IMsBoxWindow<T> dlg)
        {
            return dlg.ShowDialog(_window);
        }

        private readonly Window _window;
    }
}
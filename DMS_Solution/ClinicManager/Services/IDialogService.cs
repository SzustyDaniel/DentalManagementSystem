using System.Windows;

namespace ClinicManager.Services
{
    public interface IDialogService
    {
        MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon);
    }
}
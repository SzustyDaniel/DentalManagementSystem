using System.Windows;

namespace ClinicManager.Services
{
    class DialogService : IDialogService
    {
        public MessageBoxResult ShowMessageBox(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return MessageBox.Show(messageBoxText, caption, button, icon);
        }
    }
}

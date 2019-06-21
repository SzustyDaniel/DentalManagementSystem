using System.Windows.Controls;

namespace StaffStationClient.Services
{
    public interface IDialogService
    {
        UserControl GetUserControl(ViewType type);
        void ShowMessage(string message);
    }
}
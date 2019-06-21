using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace QueueRegisteringClient.Services
{
    public interface IViewsDialog
    {
        UserControl ChangeCurrentView(ViewType view);
        void ShowErrorDialog(string message);
    }
}

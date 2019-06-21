using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Views;

namespace QueueRegisteringClient.Tests.Mocks
{
    public class ViewDialogMockup : IViewsDialog
    {
        public System.Windows.Controls.UserControl ChangeCurrentView(ViewType view)
        {
            switch (view)
            {
                case ViewType.welcome:
                    return new WelcomeComponent();
                case ViewType.select:
                    return new SelectQueueComponent();
                case ViewType.display:
                    return new QueueDetailsDisplayComponent();
                case ViewType.none:
                    throw new ArgumentException("Passed type was not set");
                default:
                    throw new ArgumentException("Passed type is invalid");
            }
        }

        public void ShowErrorDialog(string message)
        {
        }
    }
}

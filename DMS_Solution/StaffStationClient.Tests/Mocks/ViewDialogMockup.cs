using StaffStationClient.Services;
using StaffStationClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StaffStationClient.Tests.Mocks
{
    class ViewDialogMockup : IDialogService
    {
        public UserControl GetUserControl(ViewType type)
        {
            switch (type)
            {
                case ViewType.None:
                    throw new ArgumentException("The type is not set");
                case ViewType.Login:
                    return new LoginUC();
                case ViewType.Control:
                    return new StaffControlUC();
                default:
                    throw new ArgumentException("The type is not valid");
            }
        }

        public void ShowMessage(string message)
        {
        }
    }
}

using QueueRegisteringClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace QueueRegisteringClient.Services
{
    public class ViewsDialog : IViewsDialog
    {
        private static ViewsDialog _instance;

        public static ViewsDialog Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ViewsDialog();

                return _instance;
            }
        }


        private ViewsDialog()
        {

        }


        /*
         * Return the user control to navigate to
         */
        public UserControl ChangeCurrentView(ViewType view)
        {
            UserControl currentUserControl;
            switch (view)
            {
                case ViewType.welcome:
                    return currentUserControl = new WelcomeComponent();
                case ViewType.select:
                    return currentUserControl = new SelectQueueComponent();
                case ViewType.display:
                    return currentUserControl = new QueueDetailsDisplayComponent();
                case ViewType.none:
                    throw new ArgumentException("Type not configured");
                default:
                    throw new ArgumentException("Not a valid type");
            }
        }


        public void ShowErrorDialog(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}

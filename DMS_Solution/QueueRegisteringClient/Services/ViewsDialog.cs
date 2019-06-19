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
    public static class ViewsDialog
    {
        private static Window window = null;

        /*
         * Return the user control to navigate to
         */
        public static UserControl ChangeCurrentView(ViewType view)
        {
            UserControl currentUserControl;
            switch (view)
            {
                case ViewType.welcome:
                    return currentUserControl = new WelcomeComponent();
                case ViewType.select:
                    return currentUserControl = new SelectQueueComponent();
                default:
                    throw new ArgumentException("Not a valid type");
            }
        }

        public static void ShowWindowDialog()
        {
            window = new QueueDetailsDisplay();
            window.Show();
        }

        public static void CloseWindowDialog()
        {
            if (window != null)
                window.Close();
        }

    }
}

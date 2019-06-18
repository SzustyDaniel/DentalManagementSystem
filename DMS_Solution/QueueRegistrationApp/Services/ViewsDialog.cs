using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using QueueRegistrationApp.Views;

namespace QueueRegistrationApp.Services
{
    public class ViewsDialog
    {
        private Window window = null;

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
                default:
                    throw new ArgumentException("Not a valid type");
            }
        }

        public void ShowWindowDialog()
        {
            window = new QueueDetailsDisplay();
            window.ShowDialog();
        }

        public void CloseWindowDialog()
        {
            if(window != null)
                window.Close();
        }


    }
}

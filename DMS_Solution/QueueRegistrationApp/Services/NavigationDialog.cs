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
    public class NavigationDialog
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


    }
}

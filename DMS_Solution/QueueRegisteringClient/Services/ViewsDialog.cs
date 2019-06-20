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
    public class ViewsDialog
    {
        private Window window = null;
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
                default:
                    throw new ArgumentException("Not a valid type");
            }
        }

        public void ShowWindowDialog()
        {
            window = new QueueDetailsDisplay();
            window.Show();
        }

        public void CloseWindowDialog()
        {
            if (window != null)
                window.Close();
        }

    }
}

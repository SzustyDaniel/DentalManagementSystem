using StaffStationClient.ViewModels;
using StaffStationClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StaffStationClient.Services
{
    public class DialogService : IDialogService
    {

        #region Singleton
        private static DialogService instance;

        public static DialogService Instance
        {
            get
            {
                if (instance == null)
                    instance = new DialogService();

                return instance;
            }
        }

        private DialogService(){ }
        #endregion

        /* Return the type of user control to shown on the shell */
        public UserControl GetUserControl(ViewType type)
        {
            switch (type)
            {
                case ViewType.None:
                    throw new ArgumentException("Type not configured");
                case ViewType.Login:
                    return new LoginUC();
                case ViewType.Control:
                    return new StaffControlUC();
                default:
                    throw new ArgumentException("No valid argument was passed");
            }
        }
    }
}

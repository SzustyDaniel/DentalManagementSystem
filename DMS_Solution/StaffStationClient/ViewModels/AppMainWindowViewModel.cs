using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using StaffStationClient.Views;

namespace StaffStationClient.ViewModels
{
    public class AppMainWindowViewModel : BindableBase
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value); }
        }

        public AppMainWindowViewModel()
        {
            CurrentView = new LoginUC();
        }

        private void SwitchView(UserControl uc)
        {
            CurrentView = uc;
        }
    }
}

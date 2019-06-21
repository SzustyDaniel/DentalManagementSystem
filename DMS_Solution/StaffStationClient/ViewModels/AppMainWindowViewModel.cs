using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using StaffStationClient.Views;
using Prism.Events;
using StaffStationClient.Utility;
using StaffStationClient.Services;

namespace StaffStationClient.ViewModels
{
    public class AppMainWindowViewModel : BindableBase
    {
        private IDialogService dialogService;

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value); }
        }

        public AppMainWindowViewModel(IEventAggregator ea)
        {
            dialogService = DialogService.Instance;
            CurrentView = dialogService.GetUserControl(ViewType.Login);
            ea.GetEvent<ChangeViewEvent>().Subscribe(SwitchView);
        }

        private void SwitchView(ViewType type)
        {
            CurrentView = dialogService.GetUserControl(type);
        }
    }
}

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
using StaffStationClient.Models;

namespace StaffStationClient.ViewModels
{
    public class AppMainWindowViewModel : BindableBase
    {
        private IDialogService dialogService;
        private IHttpActions httpActions;

        private UserControl _currentView;
        public UserControl CurrentView
        {
            get { return _currentView; }
            set { SetProperty(ref _currentView, value); }
        }

        public StationModel Model { get; set; }

        public AppMainWindowViewModel(IEventAggregator ea,IHttpActions httpActions , IDialogService dialogService)
        {
            this.dialogService = dialogService;
            this.httpActions = httpActions;

            CurrentView = dialogService.GetUserControl(ViewType.Login);
            ea.GetEvent<ChangeViewEvent>().Subscribe(SwitchView);
            ea.GetEvent<SendModelEvent>().Subscribe(LoadModel);
            ea.GetEvent<LogUserForceEvent>().Subscribe(ForceQuit);
        }

        private void ForceQuit()
        {
            try
            {
                if (Model != null)
                    httpActions.LogOutAsync(Model.UserName);
            }
            catch (Exception e)
            {
                dialogService.ShowMessage(e.Message);
            }
        }

        private void LoadModel(StationModel obj)
        {
            Model = obj;
        }

        private void SwitchView(ViewType type)
        {
            CurrentView = dialogService.GetUserControl(type);
        }
    }
}

using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Utility;
using QueueRegisteringClient.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace QueueRegisteringClient.ViewModels
{
    public class AppMainWindowViewModel : BindableBase
    {
        private UserControl currentView;
        private ViewsDialog viewsDialog;
        public UserControl CurrentView
        {
            get { return currentView; }
            set { SetProperty(ref currentView, value); }
        }

        public AppMainWindowViewModel(IEventAggregator ea)
        {
            viewsDialog = new ViewsDialog();
            CurrentView = viewsDialog.ChangeCurrentView(ViewType.welcome);
            ea.GetEvent<ChangeViewEvent>().Subscribe(ChangeCurrentView);
        }

        private void ChangeCurrentView(ViewType obj)
        {
            switch (obj)
            {
                case ViewType.welcome:
                    CurrentView = viewsDialog.ChangeCurrentView(obj);
                    break;
                case ViewType.select:
                    CurrentView = viewsDialog.ChangeCurrentView(obj);
                    break;
                default:
                    throw new ApplicationException("Didn't receive a valid type");
            }
        }
    }
}

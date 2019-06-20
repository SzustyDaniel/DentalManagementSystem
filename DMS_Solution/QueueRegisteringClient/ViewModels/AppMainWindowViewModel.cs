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
        private ViewsDialog views;

        private UserControl currentView;
        public UserControl CurrentView
        {
            get { return currentView; }
            set { SetProperty(ref currentView, value); }
        }


        public AppMainWindowViewModel(IEventAggregator ea)
        {
            views = ViewsDialog.Instance;
            CurrentView = views.ChangeCurrentView(ViewType.welcome);
            ea.GetEvent<ChangeViewEvent>().Subscribe(ChangeCurrentView);
        }

        private void ChangeCurrentView(ViewType obj)
        {
            switch (obj)
            {
                case ViewType.welcome:
                    CurrentView = views.ChangeCurrentView(obj);
                    break;
                case ViewType.select:
                    CurrentView = views.ChangeCurrentView(obj);
                    break;
                case ViewType.display:
                    CurrentView = views.ChangeCurrentView(obj);
                    break;
                default:
                    throw new ApplicationException("Didn't receive a valid type");
            }
        }
    }
}

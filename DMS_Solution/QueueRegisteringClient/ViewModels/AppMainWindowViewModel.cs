using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
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
        public UserControl CurrentView
        {
            get { return currentView; }
            set { SetProperty(ref currentView, value); }
        }

        public AppMainWindowViewModel(IEventAggregator ea)
        {
            CurrentView = new WelcomeComponent();
        }
    }
}

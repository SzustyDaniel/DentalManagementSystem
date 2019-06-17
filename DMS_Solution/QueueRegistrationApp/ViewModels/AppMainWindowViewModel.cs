using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;
using QueueRegistrationApp.Views;
using Prism.Events;

namespace QueueRegistrationApp.ViewModels
{
    public class AppMainWindowViewModel : BindableBase
    {
        private UserControl currentView;
        public UserControl CurrentView
        {
            get { return currentView; }
            set { SetProperty(ref currentView, value); }
        }

        public AppMainWindowViewModel()
        {
            CurrentView = new WelcomeComponent();
        }
    }
}

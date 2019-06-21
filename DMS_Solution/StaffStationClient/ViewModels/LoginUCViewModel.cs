using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using StaffStationClient.Models;
using StaffStationClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaffStationClient.ViewModels
{
    public class LoginUCViewModel : BindableBase
    {
        private IEventAggregator eventAggregator;

        private StationModel model;
        public StationModel Model
        {
            get { return model;  }
            set { SetProperty(ref model, value); CanExecuteLoginCommand(); }
        }


        public LoginUCViewModel(IEventAggregator ea)
        {
            this.eventAggregator = ea;
            Model = new StationModel();
        }

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommand, CanExecuteLoginCommand));

        void ExecuteLoginCommand()
        {
            // TODO Enter Login call for the http client
            eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.Control);
        }

        bool CanExecuteLoginCommand()
        {
            try
            {
                if (Model.Password.Length == 0 || Model.UserName.Length == 0)
                    return false;

                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }

        }
    }
}

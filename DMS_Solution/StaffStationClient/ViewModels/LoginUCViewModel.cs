using Common;
using Common.UserModels;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using StaffStationClient.Models;
using StaffStationClient.Services;
using StaffStationClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace StaffStationClient.ViewModels
{
    public class LoginUCViewModel : BindableBase
    {
        #region Properties and fields
        // Class injections
        private IEventAggregator eventAggregator;
        private IHttpActions httpActions;
        private IDialogService dialogService;

        // Mockup 
        public List<ServiceType> ServiceTypes { get; } = new List<ServiceType>() { ServiceType.Nurse, ServiceType.Pharmacist };

        // Model
        private StationModel model;
        public StationModel Model
        {
            get { return model;  }
            set { SetProperty(ref model, value); }
        }
        #endregion

        #region Constructor

        public LoginUCViewModel(IEventAggregator ea,IHttpActions httpActions, IDialogService dialogService)
        {
            this.eventAggregator = ea;
            Model = new StationModel();
            this.httpActions = httpActions;
            this.dialogService = dialogService;
        }

        #endregion

        #region Commands

        private DelegateCommand _loginCommand;
        public DelegateCommand LoginCommand =>
            _loginCommand ?? (_loginCommand = new DelegateCommand(ExecuteLoginCommandAsync, CanExecuteLoginCommand)
            ).ObservesProperty(() => Model.UserName).ObservesProperty(() => Model.Password).ObservesProperty(() => Model.StationServiceType).ObservesProperty(() => Model.StationNumber);

        private async void ExecuteLoginCommandAsync()
        {
            try
            {
                EmployeeLogin employeeLogin = new EmployeeLogin()
                { Username = Model.UserName, Password = Model.Password, ServiceType = Model.StationServiceType, StationNumber = Model.StationNumber };
                var respons = await httpActions.SendCredentialsAsync(employeeLogin);

                Model.EmployeeId = respons.EmployeeId;
                Model.EmployeeFirstName = respons.Firstname;
                Model.EmployeeLastName = respons.Lastname;

                eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.Control);
                eventAggregator.GetEvent<SendModelEvent>().Publish(Model);
            }
            catch (HttpRequestException e)
            {
                dialogService.ShowMessage(e.Message);
            }
            
        }

        private bool CanExecuteLoginCommand()
        {
            try
            {
                if (Model.Password.Length == 0 || Model.UserName.Length == 0 || Model.StationNumber == 0 || Model.StationServiceType == ServiceType.none)
                    return false;

                return true;
            }
            catch (NullReferenceException)
            {
                return false;
            }

        }

        #endregion
    }
}

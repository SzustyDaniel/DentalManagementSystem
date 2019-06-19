using Prism.Commands;
using Prism.Mvvm;
using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StaffStationClient.ViewModels
{
    public class StaffControlUCViewModel : BindableBase
    {
        #region Properties

        private StationModel model;
        public StationModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        private string stationType;
        public string SationTypeText
        {
            get { return stationType; }
            set { SetProperty(ref stationType, value); }
        }

        private string stationNumber;
        public string StationNumber
        {
            get { return stationNumber; }
            set { SetProperty(ref stationNumber, value); }
        }

        #endregion


        public StaffControlUCViewModel()
        {
            Model = new StationModel() { StationServiceType = Common.ServiceType.Pharmacist, StationNumber = 15 };
            StationNumber = $"Station Number: {Model.StationNumber}";
            SationTypeText = $"Station Type: {Model.StationServiceType}";
        }

        #region Commands

        private DelegateCommand _callNextCommand;
        public DelegateCommand CallNextCommand =>
            _callNextCommand ?? (_callNextCommand = new DelegateCommand(ExecuteCallNextCommand, CanExecuteCallNextCommand));

        void ExecuteCallNextCommand()
        {
            throw new NotImplementedException();
        }

        bool CanExecuteCallNextCommand()
        {
            return true;
        }

        private DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new DelegateCommand(ExecuteLogoutCommand, CanExecuteLogoutCommand));

        void ExecuteLogoutCommand()
        {
            throw new NotImplementedException();
        }

        bool CanExecuteLogoutCommand()
        {
            return true;
        }

        #endregion

    }
}

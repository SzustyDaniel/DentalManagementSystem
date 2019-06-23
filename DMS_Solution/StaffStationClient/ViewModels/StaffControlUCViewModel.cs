using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using StaffStationClient.Services;
using StaffStationClient.Utility;
using Common.QueueModels;

namespace StaffStationClient.ViewModels
{
    public class StaffControlUCViewModel : BindableBase
    {
        #region Properties

        private IHttpActions http;
        private IDialogService dialog;
        private IEventAggregator aggregator;

        private StationModel model;
        public StationModel Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        private DequeueModel dequeueModel;
        public DequeueModel DequeueModel
        {
            get { return dequeueModel; }
            set { SetProperty(ref dequeueModel, value); }
        }

        #endregion


        public StaffControlUCViewModel(IEventAggregator eventAggregator, IHttpActions httpActions, IDialogService dialogService)
        {
            // assign injections
            aggregator = eventAggregator;
            http = httpActions;
            dialog = dialogService;

            // Assaign for properties
            aggregator.GetEvent<SendModelEvent>().Subscribe(LoadModel);
            DequeueModel = new DequeueModel();
        }

        private void LoadModel(StationModel obj)
        {
            Model = obj;
        }

        #region Commands

        private DelegateCommand _callNextCommand;
        public DelegateCommand CallNextCommand =>
            _callNextCommand ?? (_callNextCommand = new DelegateCommand(ExecuteCallNextCommandAsync, CanExecuteCallNextCommand));

        async void ExecuteCallNextCommandAsync()
        {
            try
            {
                DequeuePosition dequeuePosition = new DequeuePosition() { ServiceType = Model.StationServiceType, StationNumber = Model.StationNumber };

                DequeuePositionResult result = await http.CallNextInQueueAsync(dequeuePosition);
                DequeueModel.CustomerId = result.CustomerID;
                DequeueModel.QueueuNumber = result.CustomerNumberInQueue;
            }
            catch (Exception e)
            {
                dialog.ShowMessage(e.Message);
            }
        }

        bool CanExecuteCallNextCommand()
        {
            return true;
        }

        private DelegateCommand _logoutCommand;
        public DelegateCommand LogoutCommand =>
            _logoutCommand ?? (_logoutCommand = new DelegateCommand(ExecuteLogoutCommandAsync, CanExecuteLogoutCommand));

        async void ExecuteLogoutCommandAsync()
        {
            await http.LogOutAsync(Model.UserName);
            aggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.Login);
        }

        bool CanExecuteLogoutCommand()
        {
            return true;
        }

        #endregion

    }
}

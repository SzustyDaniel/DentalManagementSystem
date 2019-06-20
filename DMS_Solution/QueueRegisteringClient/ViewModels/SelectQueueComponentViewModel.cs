﻿using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Models;
using QueueRegisteringClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using Common.QueueModels;
using QueueRegisteringClient.Services;

namespace QueueRegisteringClient.ViewModels
{
    public class SelectQueueComponentViewModel : BindableBase
    {

        #region Properties
        private ViewsDialog views;
        private IEventAggregator eventAggregator;
        private ClientHttpActions clientHttp;

        private Patient _model;
        public Patient Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        #endregion

        #region Constructor

        public SelectQueueComponentViewModel(IEventAggregator ea)
        {
            clientHttp = ClientHttpActions.Instance;
            eventAggregator = ea;
            views = ViewsDialog.Instance;
            eventAggregator.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
        }

        #endregion     

        #region commands
        // Nurse queue enter command
        private DelegateCommand _enterNurseQueue;
        public DelegateCommand EnterNurseQueueCommand =>
            _enterNurseQueue ?? (_enterNurseQueue = new DelegateCommand(ExecuteEnterNurseQueueCommandAsync, CanExecuteEnterNurseQueueCommand));

        private void ExecuteEnterNurseQueueCommandAsync()
        {
            EnterToQueueActions(ServiceType.Nurse);
        }

        bool CanExecuteEnterNurseQueueCommand()
        {
            return true;
        }

        // Pharmacist queue enter command
        private DelegateCommand _enterPharmacyQueue;
        public DelegateCommand EnterPharmacyQueueCommand =>
            _enterPharmacyQueue ?? (_enterPharmacyQueue = new DelegateCommand(ExecuteEnterPharmacyQueueCommandAsync, CanExecuteEnterPharmacyQueueCommand));

        private void ExecuteEnterPharmacyQueueCommandAsync()
        {
            EnterToQueueActions(ServiceType.Pharmacist);
        }

        bool CanExecuteEnterPharmacyQueueCommand()
        {
            return true;
        }
        #endregion

        #region Methods

        /*
         * Catch model sent from another view-model
         */
        private void LoadModel(Patient obj)
        {
            Model = obj;
            eventAggregator.GetEvent<SendPatientEvent>().Unsubscribe(LoadModel); // stop listening to the event
        }

        /*
         * Enter the client to the appropriate queue
         */
        private async void EnterToQueueActions(ServiceType service)
        {
            EnqueuePosition enqueuePosition = new EnqueuePosition() { ServiceType = service, UserID = Model.CustomerID };
            Model.LineNumber = await clientHttp.RegisterToQueueAsync(enqueuePosition);
            Model.QueueType = service;

            eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.display);  // switch the current view to display
            eventAggregator.GetEvent<SendPatientEvent>().Publish(Model);            // send it the current model
            
        }

        #endregion
    }
}

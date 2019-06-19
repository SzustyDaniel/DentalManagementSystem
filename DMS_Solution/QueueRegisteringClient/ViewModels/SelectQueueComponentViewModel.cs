using Prism.Commands;
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

        private IEventAggregator _ea;
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
            _ea = ea;
            _ea.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
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
            _ea.GetEvent<SendPatientEvent>().Unsubscribe(LoadModel); // stop listening to the event
        }

        /*
         * Enter the client to the appropriate queue
         */
        private async void EnterToQueueActions(ServiceType service)
        {
            EnqueuePosition enqueuePosition = new EnqueuePosition() { ServiceType = service, UserID = Model.CustomerID };
            Model.LineNumber = await clientHttp.RegisterToQueueAsync(enqueuePosition);
            Model.QueueType = service;

            ViewsDialog.ShowWindowDialog();                             // open information window for the user
            _ea.GetEvent<SendPatientEvent>().Publish(Model);            // send it the current model
            _ea.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);  // switch the current view to welcome
        }

        #endregion
    }
}

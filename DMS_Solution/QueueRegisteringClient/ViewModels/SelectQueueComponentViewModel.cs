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
using System.Net.Http;

namespace QueueRegisteringClient.ViewModels
{
    public class SelectQueueComponentViewModel : BindableBase
    {

        #region Properties
        private IViewsDialog views;
        private IEventAggregator eventAggregator;
        private IClientHttpActions clientHttp;

        private Patient _model;
        public Patient Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        #endregion

        #region Constructor

        public SelectQueueComponentViewModel(IEventAggregator ea,IClientHttpActions clientHttpActions, IViewsDialog viewsDialog)
        {
            clientHttp = clientHttpActions;
            eventAggregator = ea;
            views = viewsDialog;
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
            EnterToQueueActionsAsync(ServiceType.Nurse);
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
            EnterToQueueActionsAsync(ServiceType.Pharmacist);
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
        private async void EnterToQueueActionsAsync(ServiceType service)
        {
            try
            {
                EnqueuePosition enqueuePosition = new EnqueuePosition() { ServiceType = service, UserID = Model.CustomerID };
                Model.LineNumber = await clientHttp.RegisterToQueueAsync(enqueuePosition);
                Model.QueueType = service;

                eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.display);  // switch the current view to display
                eventAggregator.GetEvent<SendPatientEvent>().Publish(Model);            // send it the current model
            }
            catch (HttpRequestException e)
            {
                views.ShowErrorDialog(e.Message);
                eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);  // switch the current view to display
            }
            
            
        }

        #endregion
    }
}

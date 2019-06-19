using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Models;
using QueueRegisteringClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueRegisteringClient.ViewModels
{
    public class SelectQueueComponentViewModel : BindableBase
    {
        private IEventAggregator _ea;

        private Patient _model;
        public Patient Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public SelectQueueComponentViewModel(IEventAggregator ea)
        {
            _ea = ea;
            _ea.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
        }

        // receive a model from another view-model
        private void LoadModel(Patient obj)
        {
            Model = obj;
            _ea.GetEvent<SendPatientEvent>().Unsubscribe(LoadModel); // stop listening to the event
        }

        #region commands
        private DelegateCommand _enterNurseQueue;
        public DelegateCommand EnterNurseQueueCommand =>
            _enterNurseQueue ?? (_enterNurseQueue = new DelegateCommand(ExecuteEnterNurseQueueCommand, CanExecuteEnterNurseQueueCommand));

        void ExecuteEnterNurseQueueCommand()
        {
            throw new NotImplementedException();
        }

        bool CanExecuteEnterNurseQueueCommand()
        {
            return true;
        }

        private DelegateCommand _enterPharmacyQueue;

        public DelegateCommand EnterPharmacyQueueCommand =>
            _enterPharmacyQueue ?? (_enterPharmacyQueue = new DelegateCommand(ExecuteEnterPharmacyQueueCommand, CanExecuteEnterPharmacyQueueCommand));

        void ExecuteEnterPharmacyQueueCommand()
        {
            throw new NotImplementedException();
        }

        bool CanExecuteEnterPharmacyQueueCommand()
        {
            return true;
        }
        #endregion
    }
}

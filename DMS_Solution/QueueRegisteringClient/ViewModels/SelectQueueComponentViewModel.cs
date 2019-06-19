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
        private IEventAggregator _ea;
        private ClientHttpActions clientHttp;

        private Patient _model;
        public Patient Model
        {
            get { return _model; }
            set { SetProperty(ref _model, value); }
        }

        public SelectQueueComponentViewModel(IEventAggregator ea)
        {
            clientHttp = ClientHttpActions.Instance;
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
            _enterNurseQueue ?? (_enterNurseQueue = new DelegateCommand(ExecuteEnterNurseQueueCommandAsync, CanExecuteEnterNurseQueueCommand));

        async void ExecuteEnterNurseQueueCommandAsync()
        {
            EnqueuePosition enqueuePosition = new EnqueuePosition() { ServiceType = ServiceType.Nurse, UserID = Model.CustomerID };
            Model.LineNumber = await clientHttp.RegisterToQueueAsync(enqueuePosition);
            ViewsDialog.ShowWindowDialog();
            _ea.GetEvent<SendPatientEvent>().Publish(Model);
            _ea.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);
        }

        bool CanExecuteEnterNurseQueueCommand()
        {
            return true;
        }

        private DelegateCommand _enterPharmacyQueue;

        public DelegateCommand EnterPharmacyQueueCommand =>
            _enterPharmacyQueue ?? (_enterPharmacyQueue = new DelegateCommand(ExecuteEnterPharmacyQueueCommandAsync, CanExecuteEnterPharmacyQueueCommand));

        async void ExecuteEnterPharmacyQueueCommandAsync()
        {
            EnqueuePosition enqueuePosition = new EnqueuePosition() { ServiceType = ServiceType.Pharmacist, UserID = Model.CustomerID };
            Model.LineNumber = await clientHttp.RegisterToQueueAsync(enqueuePosition);
            ViewsDialog.ShowWindowDialog();
            _ea.GetEvent<SendPatientEvent>().Publish(Model);
            _ea.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);
        }

        bool CanExecuteEnterPharmacyQueueCommand()
        {
            return true;
        }
        #endregion
    }
}

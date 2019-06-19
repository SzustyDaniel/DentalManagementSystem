using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueRegisteringClient.ViewModels
{
    public class SelectQueueComponentViewModel : BindableBase
    {
        public SelectQueueComponentViewModel()
        {
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

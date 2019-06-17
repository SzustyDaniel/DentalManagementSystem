using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueRegistrationApp.ViewModels
{
    public class SelectQueueComponentViewModel : BindableBase
    {
        public string Message { get; set; }
        public string FirstService { get; set; }
        public string SecondService { get; set; }


        public SelectQueueComponentViewModel()
        {
            Message = @"Select the service you wish to recieve";
            FirstService = "Pharmacy";
            SecondService = "Nures treatment";
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

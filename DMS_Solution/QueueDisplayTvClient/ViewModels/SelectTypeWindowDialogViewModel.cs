using Common;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueDisplayTvClient.ViewModels
{
    public class SelectTypeWindowDialogViewModel : BindableBase
    {
        public ServiceType DialogResult { get; set; }

        public SelectTypeWindowDialogViewModel()
        {

        }

        private DelegateCommand<string> _selectServiceCommand;
        public DelegateCommand<string> SelectServiceCommand =>
            _selectServiceCommand ?? (_selectServiceCommand = new DelegateCommand<string>(ExecuteSelectServiceCommand));

        void ExecuteSelectServiceCommand(string parameter)
        {
            switch (parameter)
            {
                case "Pharmacy":
                    DialogResult = ServiceType.Pharmacist;
                    break;
                case "Nurse":
                    DialogResult = ServiceType.Nurse;
                    break;
                default:
                    break;
            }
        }
    }
}

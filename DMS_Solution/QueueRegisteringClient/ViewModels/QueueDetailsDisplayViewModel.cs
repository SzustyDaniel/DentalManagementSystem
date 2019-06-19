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
    public class QueueDetailsDisplayViewModel : BindableBase
    {
        #region Properties
        private Patient model;
        public Patient Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        #endregion

        public QueueDetailsDisplayViewModel(IEventAggregator ea)
        {
            ea.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
        }

        private void LoadModel(Patient obj)
        {
            Model = obj;
        }

    }
}

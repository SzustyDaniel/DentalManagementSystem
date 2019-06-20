using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Models;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace QueueRegisteringClient.ViewModels
{
    public class QueueDetailsDisplayComponentViewModel : BindableBase
    {
          #region Properties
        private IEventAggregator eventAggregator;

        private Patient model;
        public Patient Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        #endregion

        public QueueDetailsDisplayComponentViewModel(IEventAggregator ea)
        {
            eventAggregator = ea;
            ea.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
        }
        

        private void LoadModel(Patient obj)
        {
            Model = obj;
        }

        private void SetForSwitch()
        {
            Thread.Sleep(3000);
            eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);
        }
    }
}

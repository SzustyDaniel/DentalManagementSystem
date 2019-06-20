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
using System.Windows.Threading;

namespace QueueRegisteringClient.ViewModels
{
    public class QueueDetailsDisplayComponentViewModel : BindableBase
    {
          #region Properties
        private IEventAggregator eventAggregator;
        private DispatcherTimer dispatcherTimer;

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

            // For timed switch of the views in the application main window
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromSeconds(3);
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            ea.GetEvent<SendPatientEvent>().Subscribe(LoadModel);
        }

        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            eventAggregator.GetEvent<ChangeViewEvent>().Publish(ViewType.welcome);
            dispatcherTimer.Stop();
        }

        // loads the received model from the event aggregator
        private void LoadModel(Patient obj)
        {
            Model = obj;
            dispatcherTimer.Start();
        }
    }
}

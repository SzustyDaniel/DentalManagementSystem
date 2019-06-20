using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Models;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;

namespace QueueRegisteringClient.ViewModels
{
    public class QueueDetailsDisplayViewModel : BindableBase
    {
        #region Properties
        private ViewsDialog views;
        //private Timer closingTimer;

        private Patient model;
        public Patient Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }
        #endregion

        public QueueDetailsDisplayViewModel(IEventAggregator ea)
        {
            views = ViewsDialog.Instance;
            /*
            closingTimer = new Timer(3000);
            closingTimer.Elapsed += CloseWindowEvent;
            */
            ea.GetEvent<SendPatientEvent>().Subscribe(LoadModel);

        }

        /*
        private void CloseWindowEvent(object sender, ElapsedEventArgs e)
        {
            closingTimer.Close();
            views.CloseWindowDialog();
        }
        */

        private void LoadModel(Patient obj)
        {
            Model = obj;
            //closingTimer.Start();
        }

    }
}

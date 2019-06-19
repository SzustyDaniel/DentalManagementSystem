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
        private Patient model;
        public Patient Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        private string _lineNumbertxt;
        public string LineNumberText
        {
            get { return _lineNumbertxt; }
            set { SetProperty(ref _lineNumbertxt, value); }
        }

        private string _lineTypeTxt;
        public string LineTypeText
        {
            get { return _lineTypeTxt; }
            set { SetProperty(ref _lineTypeTxt, value); }
        }


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

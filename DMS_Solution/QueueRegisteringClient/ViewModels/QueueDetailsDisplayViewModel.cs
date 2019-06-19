using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using QueueRegisteringClient.Models;
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

        public string LineNumberText { get; set; }
        public string LineTypeText { get; set; }

        public QueueDetailsDisplayViewModel(IEventAggregator ea)
        {

        }

        public QueueDetailsDisplayViewModel(Patient patient)
        {
            Model = patient;
            LineTypeText = $"In line for {Model.QueueType}";
            LineNumberText = $"Number in line: {Model.LineNumber.UserNumber}";
        }
    }
}

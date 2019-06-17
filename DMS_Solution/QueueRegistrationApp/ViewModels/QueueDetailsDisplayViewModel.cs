using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using QueueRegistrationApp.Models;

namespace QueueRegistrationApp.ViewModels
{
    public class QueueDetailsDisplayViewModel : BindableBase
    {
        private Patient model;
        public Patient Model
        {
            get { return model; }
            set { SetProperty(ref model, value); }
        }

        public string NameText { get; set; }
        public string LineNumberText { get; set; }
        public string LineTypeText { get; set; }

        public QueueDetailsDisplayViewModel()
        {
            //Mockup code
            Model = new Patient() { FirstName = "Roni", LastName = "Szuster", LineNumber = 10, QueueType = "Nurse" };
            NameText = $"{Model.FirstName} {Model.LastName}";
            LineNumberText = $"Number in line: {Model.LineNumber}";
            LineTypeText = $"In line for {Model.QueueType}";
        }

        public QueueDetailsDisplayViewModel(Patient patient)
        {
            Model = patient;
            NameText = $"{Model.FirstName} {Model.LastName}";
            LineNumberText = $"Number in line: {Model.LineNumber}";
            LineTypeText = $"In line for {Model.QueueType}";
        }
    }
}

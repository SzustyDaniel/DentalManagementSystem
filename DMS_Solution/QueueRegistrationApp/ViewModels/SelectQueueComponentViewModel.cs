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
    }
}

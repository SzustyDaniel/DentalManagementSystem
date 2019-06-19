using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QueueRegisteringClient.ViewModels
{
    public class WelcomeComponentViewModel : BindableBase
    {
        public string Message { get; set; }

        public WelcomeComponentViewModel()
        {
            Message = @"Welcome to the clinic.
Please swipe your card to continue...";

        }
    }
}

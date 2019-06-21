using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using QueueRegisteringClient.Services;
using Common;
using Common.UserModels;
using QueueRegisteringClient.Models;
using Prism.Events;
using QueueRegisteringClient.Utility;
using System.Net.Http;

namespace QueueRegisteringClient.ViewModels
{
    public class WelcomeComponentViewModel : BindableBase
    {
        public string Message { get; set; }
        public List<ulong> MockUsers { get; set; }
        public ulong SelectedUser { get; set; }

        private IViewsDialog views;
        private IEventAggregator _ea; // event aggregation publisher
        private IClientHttpActions httpActions;
        public Patient Customer { get; set; } // The user model for the system

        
        public WelcomeComponentViewModel(IEventAggregator ea)
        {
            _ea = ea;
            httpActions = ClientHttpActions.Instance;
            Customer = new Patient();

            Message = @"Welcome to the clinic.
Please swipe your card to continue...";

            //Mock Data for the application
            MockUsers = new List<ulong>(){  200,300,400,500,600,700,800,900,1000}; // simulates card swipe information
            SelectedUser = 0;
            views = ViewsDialog.Instance;
        }

        // for unit testing
        public WelcomeComponentViewModel(IClientHttpActions clientHttpActions, IViewsDialog viewsDialog)
        {
            httpActions = clientHttpActions;
            views = viewsDialog;
            Customer = new Patient();
        }

     


        private DelegateCommand _sendValidateCommand;
        public DelegateCommand SendValidateCommand =>
            _sendValidateCommand ?? (_sendValidateCommand = new DelegateCommand(ExecuteSendValidateCommandAsync));

        /*
         * Simulate card swiping and connecting to the user api to check on the guid of the customer
         */
        private async void ExecuteSendValidateCommandAsync()
        {
            Customer.ClientCard = new CardInfo() { CardNumber = SelectedUser };
            try
            {
                CustomerIdentification customer = await httpActions.ValidateCustomerAsync(Customer.ClientCard);
                Customer.CustomerID = customer.CustomerId;
                _ea?.GetEvent<ChangeViewEvent>().Publish(ViewType.select);
                _ea?.GetEvent<SendPatientEvent>().Publish(Customer);
            }
            catch(HttpRequestException e)
            {
                views.ShowErrorDialog(e.Message);
            }




        }
    }
}

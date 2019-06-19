using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using QueueRegisteringClient.Services;
using Common;
using Common.UserModels;

namespace QueueRegisteringClient.ViewModels
{
    public class WelcomeComponentViewModel : BindableBase
    {
        public string Message { get; set; }
        public List<ulong> MockUsers { get; set; }
        public ulong SelectedUser { get; set; }
        private ClientHttpActions httpActions;



        public WelcomeComponentViewModel()
        {
            httpActions = ClientHttpActions.Instance;

            Message = @"Welcome to the clinic.
Please swipe your card to continue...";

            //Mock Data for the application
            MockUsers = new List<ulong>(){  200,300,400,500,600,700,800,900,1000};
            SelectedUser = 0;

        }

        private DelegateCommand _sendValidateCommand;
        public DelegateCommand SendValidateCommand =>
            _sendValidateCommand ?? (_sendValidateCommand = new DelegateCommand(ExecuteSendValidateCommandAsync));

        async void ExecuteSendValidateCommandAsync()
        {
            CardInfo cardInfo = new CardInfo() { CardNumber = SelectedUser };
            CustomerRespone customer = await httpActions.ValidateCustomer(cardInfo);
        }
    }
}

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Tests.Mocks;
using QueueRegisteringClient.ViewModels;
using QueueRegisteringClient.Models;
using Prism.Commands;

namespace QueueRegisteringClient.Tests
{
    [TestClass]
    public class WelcomeComponentTests
    {
        private IClientHttpActions httpService;
        private IViewsDialog dialogService;

        [TestInitialize]
        public void Init()
        {
            httpService = new ClientHttpActionsMockup();
            dialogService = null;
        }

        [TestMethod]
        public void ExecuteSendValidateCommandAsync_GetUserID_SetsModelUserID()
        {
            //Arrange
            WelcomeComponentViewModel viewModel = new WelcomeComponentViewModel(httpService, dialogService);
            Patient patient = viewModel.Customer;
            viewModel.SelectedUser = 200;


            //Act
            DelegateCommand command = viewModel.SendValidateCommand;
            command.Execute();

            // assert
            Assert.AreEqual(1,viewModel.Customer.CustomerID);

        }


    }
}

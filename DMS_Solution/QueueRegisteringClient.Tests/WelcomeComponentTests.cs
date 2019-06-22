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
            dialogService = new ViewDialogMockup();
        }

        [DataTestMethod]
        [DataRow(1,200)]
        [DataRow(2,300)]
        [DataRow(0,1100)]
        public void ExecuteSendValidateCommandAsync_GetUserID_SetsModelUserID(int expected,int tested)
        {
            //Arrange
            WelcomeComponentViewModel viewModel = new WelcomeComponentViewModel(null,httpService, dialogService);
            Patient patient = viewModel.Customer;
            viewModel.SelectedUser = (ulong)tested;


            //Act
            DelegateCommand command = viewModel.SendValidateCommand;
            command.Execute();

            // assert
            Assert.AreEqual(expected,viewModel.Customer.CustomerID);
        }

    }
}

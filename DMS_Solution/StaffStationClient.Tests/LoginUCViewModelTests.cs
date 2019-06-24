using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using StaffStationClient.Services;
using StaffStationClient.Tests.Mocks;
using StaffStationClient.ViewModels;

namespace StaffStationClient.Tests
{
    [TestClass]
    public class LoginUCViewModelTests
    {
        private IEventAggregator eventAggregator;
        private IHttpActions httpActions;
        private IDialogService dialogService;

        [TestInitialize]
        public void Init()
        {
            eventAggregator = ApplicationServiceMockup.Instance.EventAggregator;
            httpActions = new ClientHttpActionsMockup();
            dialogService = new ViewDialogMockup();
        }

        [TestMethod]
        public void CanExecuteLoginCommand_TestifStateChanges_StateChangedTrue()
        {
            // Arrange
            LoginUCViewModel viewModel = new LoginUCViewModel(eventAggregator,httpActions,dialogService);
            viewModel.Model.StationNumber = 1;
            viewModel.Model.StationServiceType = Common.ServiceType.Nurse;
            viewModel.Model.Password = "1234";
            viewModel.Model.UserName = "Daniel_s";

            // Act
            var test = viewModel.LoginCommand.CanExecute();

            // Assert
            Assert.IsTrue(test);
        }

        [TestMethod]
        public void CanExecuteLoginCommand_TestifStateChanges_StateChangedFalse()
        {
            // Arrange
            LoginUCViewModel viewModel = new LoginUCViewModel(eventAggregator, httpActions, dialogService);
            viewModel.Model.StationNumber = 1;
            viewModel.Model.StationServiceType = Common.ServiceType.Nurse;
            viewModel.Model.UserName = "Daniel_s";

            // Act
            var test = viewModel.LoginCommand.CanExecute();

            // Assert
            Assert.IsFalse(test);
        }
    }
}

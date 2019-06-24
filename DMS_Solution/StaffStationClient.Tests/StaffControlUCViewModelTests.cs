using System;
using Common.QueueModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using StaffStationClient.Models;
using StaffStationClient.Services;
using StaffStationClient.Tests.Mocks;
using StaffStationClient.ViewModels;

namespace StaffStationClient.Tests
{
    [TestClass]
    public class StaffControlUCViewModelTests
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
        public void LoadModel_TestIfModelSentIsSavedInViewModel_StationModelIsRecived()
        {
            //Arrange
            StaffControlUCViewModel viewModel = new StaffControlUCViewModel(eventAggregator, httpActions, dialogService);
            EventFireMockup eventFire = new EventFireMockup(eventAggregator);

            // Act
            eventFire.SendModel();

            //Assert
            Assert.AreEqual(eventFire.StationModel, viewModel.Model);
        }

        [TestMethod]
        public void ExecuteCallNextCommandAsync_CallToNextClient_NextClientWasCalled()
        {
            // Arrange
            StaffControlUCViewModel viewModel = new StaffControlUCViewModel(eventAggregator, httpActions, dialogService);
            viewModel.Model = new Models.StationModel() { StationNumber = 1, StationServiceType = Common.ServiceType.Nurse };
            EventFireMockup eventFire = new EventFireMockup(eventAggregator);

            DequeueModel result = new DequeueModel() { CustomerId = 1, QueueuNumber= 1 };

            // Act
            eventFire.SendModel();
            viewModel.CallNextCommand.Execute();

            // Assert
            Assert.AreEqual(result.CustomerId, viewModel.DequeueModel.CustomerId);
            Assert.AreEqual(result.QueueuNumber, viewModel.DequeueModel.QueueuNumber);
        }


    }
}

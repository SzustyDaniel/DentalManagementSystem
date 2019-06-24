using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
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
        public void ExecuteCallNextCommandAsync_CallToNextClient_NextClientWasCalled()
        {
            // Arrange
            StaffControlUCViewModel viewModel = new StaffControlUCViewModel(eventAggregator, httpActions, dialogService);
            viewModel.Model = new Models.StationModel() { StationNumber = 1, StationServiceType = Common.ServiceType.Nurse };

            // Act


            // Assert
        }
    }
}

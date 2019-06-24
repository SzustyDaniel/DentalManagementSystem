using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Tests.Mocks;
using QueueRegisteringClient.ViewModels;
using QueueRegisteringClient.Models;
using Common.QueueModels;
using Common.UserModels;
using Common;
using System.Threading;

namespace QueueRegisteringClient.Tests
{
    [TestClass]
    public class SelectQueueComponentViewModelTests
    {
        #region Start of tests
        private IClientHttpActions httpService;
        private IViewsDialog dialogService;
        private IEventAggregator eventAggregator;

        [TestInitialize]
        public void Init()
        {
            httpService = new ClientHttpActionsMockup();
            dialogService = new ViewDialogMockup();
            eventAggregator = ApplicationServiceMockup.Instance.EventAggregator;
        }
        #endregion

        #region Queue Action
        [DataTestMethod]
        [DataRow(ServiceType.Nurse)]
        public void ExecuteEnterNurseQueueCommandAsync_EnterUserToNurseQueue_SetsModelQueueTypeToNurse(ServiceType expected)
        {
            //Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);
            viewModel.Model = new Patient() { ClientCard = new CardInfo() { CardNumber = 200 }, CustomerID = 1 };

            //Act
            viewModel.EnterNurseQueueCommand.Execute();

            //Assert
            Assert.AreEqual(expected, viewModel.Model.QueueType);
        }

        [DataTestMethod]
        [DataRow(ServiceType.Pharmacist)]
        public void ExecuteEnterPharmacyQueueCommandAsync_EnterUserToPharmacyQueue_SetsModelQueueTypePharmacyType(ServiceType expected)
        {
            //Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);
            viewModel.Model = new Patient() { ClientCard = new CardInfo() { CardNumber = 200 }, CustomerID = 1 };

            //Act
            viewModel.EnterPharmacyQueueCommand.Execute();

            //Assert
            Assert.AreEqual(expected, viewModel.Model.QueueType);
        }

        [DataTestMethod]
        public void ExecuteEnterNurseQueueCommandAsync_EnterUserToNurseQueue_SetModelNumber()
        {
            //Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);
            viewModel.Model = new Patient() { ClientCard = new CardInfo() { CardNumber = 200 }, CustomerID = 1 };
            EnqueuePositionResult expected = new EnqueuePositionResult() { UserNumber = 1 };
            //Act
            viewModel.EnterNurseQueueCommand.Execute();

            //Assert
            Assert.AreEqual(expected.UserNumber, viewModel.Model.LineNumber.UserNumber);
        }

        [DataTestMethod]
        public void ExecuteEnterPharmacyQueueAsync_EnterUserToNurseQueue_SetModelNumber()
        {
            //Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);
            viewModel.Model = new Patient() { ClientCard = new CardInfo() { CardNumber = 200 }, CustomerID = 1 };
            EnqueuePositionResult expected = new EnqueuePositionResult() { UserNumber = 1 };

            //Act
            viewModel.EnterPharmacyQueueCommand.Execute();

            //Assert
            Assert.AreEqual(expected.UserNumber, viewModel.Model.LineNumber.UserNumber);
        }

        #endregion

        #region Load Model Testing

        [TestMethod]
        public void LoadModel_CheckIfModelWasLoaded_ModelLoaded()
        {
            //Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);
            EventFireMockup eventFire = new EventFireMockup(eventAggregator);
            eventFire.Model = new Patient() { ClientCard = new CardInfo() { CardNumber = 200 }, CustomerID = 1 };

            // Act
            eventFire.SendModel();

            // Assert
            Assert.AreEqual(eventFire.Model, viewModel.Model);
        }

        [TestMethod]
        public void LoadModel_CheckIfModelWasNotLoaded_ModelIsNull()
        {
            // Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator,httpService,dialogService);

            // Assert
            Assert.AreEqual(null, viewModel.Model);
        }

        #endregion

        #region Schedule Tests

        [TestMethod]
        public void GetScheduleAsync_TestIfTheNurseScheduleWasLoaded_NuresScheduleLoaded()
        {
            // Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);

            // Assert
            Assert.IsNotNull(viewModel.NurseScheduleModel);
        }

        [TestMethod]
        public void GetScheduleAsync_TestIfThePharmacyScheduleWasLoaded_PharmacyScheduleLoaded()
        {
            // Arrange
            SelectQueueComponentViewModel viewModel = new SelectQueueComponentViewModel(eventAggregator, httpService, dialogService);

            // Assert
            Assert.IsNotNull(viewModel.PharmacyScheduleModel);
        }

        #endregion
    }
}

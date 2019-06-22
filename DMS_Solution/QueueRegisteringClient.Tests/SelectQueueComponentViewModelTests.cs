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

namespace QueueRegisteringClient.Tests
{
    [TestClass]
    public class SelectQueueComponentViewModelTests
    {
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
    }
}

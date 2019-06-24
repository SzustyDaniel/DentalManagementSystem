using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Tests.Mocks;
using QueueRegisteringClient.ViewModels;

namespace QueueRegisteringClient.Tests
{
    [TestClass]
    public class QueueDetailsDisplayComponentViewModelTests
    {
        private IEventAggregator eventAggregator;
        private IClientHttpActions httpActions;
        private IViewsDialog dialogService;
        
        [TestInitialize]
        public void Init()
        {
            eventAggregator = ApplicationServiceMockup.Instance.EventAggregator;
            httpActions = new ClientHttpActionsMockup();
            dialogService = new ViewDialogMockup();
        }

        [TestMethod]
        public void LoadModel_TestIfTheModelWasLoaded_ModelLoaded()
        {
            // Arrange
            QueueDetailsDisplayComponentViewModel viewModel = new QueueDetailsDisplayComponentViewModel(eventAggregator);
            EventFireMockup eventFire = new EventFireMockup(eventAggregator);
            eventFire.Model = new Models.Patient()
            {
                ClientCard = new Common.UserModels.CardInfo() { CardNumber = 200 },
                CustomerID = 1,
                LineNumber = new Common.QueueModels.EnqueuePositionResult() { UserNumber = 1 },
                QueueType = Common.ServiceType.Nurse
            };

            // Act
            eventFire.SendModel();

            // Assert
            Assert.AreEqual(eventFire.Model, viewModel.Model);
        }

        [TestMethod]
        public void LoadModel_TestIfTheModelStaiesNullWithoutEventFire_ModelIsNull()
        {
            // Arrange
            QueueDetailsDisplayComponentViewModel viewModel = new QueueDetailsDisplayComponentViewModel(eventAggregator);

            // Assert
            Assert.AreEqual(null, viewModel.Model);
        }
    }
}

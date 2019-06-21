using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using QueueRegisteringClient.Services;
using QueueRegisteringClient.Tests.Mocks;

namespace QueueRegisteringClient.Tests
{
    [TestClass]
    public class AppMainWindowViewModelTests
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
        public void TestMethod1()
        {

        }
    }
}

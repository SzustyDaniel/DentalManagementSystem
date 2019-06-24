﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Prism.Events;
using StaffStationClient.Services;
using StaffStationClient.Tests.Mocks;

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
        public void TestMethod1()
        {

        }
    }
}

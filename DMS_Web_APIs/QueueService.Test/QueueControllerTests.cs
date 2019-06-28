using Common;
using Common.QueueModels;
using Common.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using NUnit.Framework;
using QueueService.AzureStorage;
using QueueService.Controller;
using QueueService.SignalR;
using QueueService.Test.Mock;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QueueService.Test
{
    [TestFixture]
    class QueueControllerTests
    {
        public static QueueController GetControllerToTest()
        {
            return new QueueController(null, new QueueStorageService(new QueueRepositoryMock()));
        }

        [Test]
        public async Task QueueControllerTest_DeletFromQueue()
        {
            var controller = GetControllerToTest();
            Assert.IsInstanceOf<BadRequestResult>((await controller.Remove(null)));
            Assert.IsInstanceOf<BadRequestResult>((await controller.Remove(new DequeuePosition { StationNumber = 1 })));

            var testResult = await controller.Remove(new DequeuePosition { ServiceType = ServiceType.Nurse });
            Assert.IsInstanceOf<OkObjectResult>(testResult);
        }

        [Test]
        public async Task QueueControllerTest_AddToQueue()
        {
            var controller = GetControllerToTest();
            Assert.IsInstanceOf<BadRequestResult>((await controller.Add(null)));
            Assert.IsInstanceOf<BadRequestResult>((await controller.Add(new EnqueuePosition { UserID = 1 })));

            var testResult = await controller.Add(new EnqueuePosition { UserID = 111, ServiceType = ServiceType.Nurse });
            Assert.IsInstanceOf<CreatedAtActionResult>(testResult);
        }

        [Test]
        public async Task QueueControllerTest_UpdateStation()
        {
            var controller = GetControllerToTest();
            Assert.IsInstanceOf<BadRequestObjectResult>((await controller.UpdateStationState(default)));
            Assert.IsInstanceOf<BadRequestObjectResult>((await controller.UpdateStationState(new EmployeeConnectionUpdate { StationNumber = 1})));
        }
    }
}

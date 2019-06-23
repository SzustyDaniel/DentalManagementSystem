using Common.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UsersService.Controllers;
using UsersService.Data;

namespace UsersService.Tests
{
    [TestFixture]
    public class UsersControllerTests
    {
        private static UsersContext GetInitializedUsersContext([CallerMemberName] string dbName = "testDB")
        {
            var options = new DbContextOptionsBuilder<UsersContext>().UseInMemoryDatabase(databaseName: dbName).Options;
            DataGenerator.Initialize(options);
            return new UsersContext(options);
        }

        [Test]
        public async Task GetCustomerId_InputExistingCardNumber_ReturnsValidCustomerId()
        {
            using (var context = GetInitializedUsersContext())
            {
                ulong existingCardNumber = 100;
                int expectedCustomerId = 1;
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);

                ActionResult<CustomerIdentification> result = await controller.GetCustomerId(existingCardNumber);

                Assert.IsInstanceOf<ActionResult<CustomerIdentification>>(result);
                Assert.IsInstanceOf<CustomerIdentification>(result.Value);
                Assert.AreEqual(expectedCustomerId, result.Value.CustomerId);
            }
        }

        [Test]
        public async Task GetCustomerId_GivenInvalidModel_ReturnsBadRequest()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                controller.ModelState.AddModelError("error", "some error");

                ActionResult<CustomerIdentification> result = await controller.GetCustomerId(0);

                Assert.IsInstanceOf<ActionResult<CustomerIdentification>>(result);
                Assert.IsInstanceOf<BadRequestObjectResult>(result.Result);
            }
        }

        [Test]
        public async Task GetCustomerId_GivenNonExistentCard_ReturnsNotFoundObjectResult()
        {
            using (var context = GetInitializedUsersContext())
            {
                ulong nonExistentCard = 999;
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);

                ActionResult<CustomerIdentification> result = await controller.GetCustomerId(nonExistentCard);

                Assert.IsInstanceOf<ActionResult<CustomerIdentification>>(result);
                Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            }
        }
    }
}

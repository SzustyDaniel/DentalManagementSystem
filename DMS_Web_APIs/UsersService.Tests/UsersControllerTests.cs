using Common;
using Common.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Linq;
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

        [Test]
        public async Task PostEmployeeLogin_FirstValidLogin_LoginSuccessful()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                EmployeeLogin login = new EmployeeLogin
                {
                    Username = "david_f",
                    Password = "password",
                    ServiceType = ServiceType.Pharmacist,
                    StationNumber = 1
                };

                ActionResult<EmployeeInfo> result = await controller.PostEmployeeLogin(login);

                Assert.IsInstanceOf<ActionResult<EmployeeInfo>>(result);
                Assert.IsInstanceOf<EmployeeInfo>(result.Value);
                bool isOnline = context.Employees.Where(e => e.Username == login.Username).Single().Online;
                Assert.IsTrue(isOnline);
            }
        }

        [Test]
        public async Task PostEmployeeLogin_LoginWhileAlreadyOnline_ReturnsUnauthorized()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                EmployeeLogin login = new EmployeeLogin
                {
                    Username = "david_f",
                    Password = "password",
                    ServiceType = ServiceType.Pharmacist,
                    StationNumber = 1
                };
                ActionResult<EmployeeInfo> firstLoginResult = await controller.PostEmployeeLogin(login);

                ActionResult<EmployeeInfo> result = await controller.PostEmployeeLogin(login);

                Assert.IsInstanceOf<ActionResult<EmployeeInfo>>(result);
                Assert.IsInstanceOf<UnauthorizedObjectResult>(result.Result);
            }
        }

        [Test]
        public async Task PostEmployeeLogin_LoginWithWrongPassword_ReturnsUnauthorizedAndEmployeeIsOffline()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                EmployeeLogin login = new EmployeeLogin
                {
                    Username = "david_f",
                    Password = "wrongPassword",
                    ServiceType = ServiceType.Pharmacist,
                    StationNumber = 1
                };

                ActionResult<EmployeeInfo> result = await controller.PostEmployeeLogin(login);

                Assert.IsInstanceOf<ActionResult<EmployeeInfo>>(result);
                Assert.IsInstanceOf<UnauthorizedObjectResult>(result.Result);
                bool isOnline = context.Employees.Where(e => e.Username == login.Username).Single().Online;
                Assert.IsFalse(isOnline);
            }
        }

        [Test]
        public async Task PostEmployeeLogout_OnlineEmployeeLogsOut_EmployeeIsOffline()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                EmployeeLogin login = new EmployeeLogin
                {
                    Username = "david_f",
                    Password = "password",
                    ServiceType = ServiceType.Pharmacist,
                    StationNumber = 1
                };
                ActionResult<EmployeeInfo> loginResult = await controller.PostEmployeeLogin(login);

                IActionResult result = await controller.PostEmployeeLogout(login.Username);

                Assert.IsInstanceOf<IActionResult>(result);
                Assert.IsInstanceOf<OkResult>(result);
                bool isOnline = context.Employees.Where(e => e.Username == login.Username).Single().Online;
                Assert.IsFalse(isOnline);
            }
        }
    }
}

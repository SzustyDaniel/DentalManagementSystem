using Common;
using Common.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UsersService.Controllers;
using UsersService.Data;
using UsersService.Data.Models;

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

        [Test]
        public async Task PostCustomerTreatment_PostValidTreatment_TreatmentSavedAndDateTruncated()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                CustomerTreatment treatment = new CustomerTreatment
                {
                    CustomerId = 10,
                    DateOfTreatment = DateTime.Now,
                    TreatingEmployeeId = 3
                };

                IActionResult result = await controller.PostCustomerTreatment(treatment);

                Assert.IsInstanceOf<IActionResult>(result);
                Assert.IsInstanceOf<CreatedAtActionResult>(result);
                Assert.DoesNotThrow(() => context.Treatments.
                    Where(t => t.CustomerId == treatment.CustomerId && t.TreatmentDate == DateTime.Today && t.EmployeeId == treatment.TreatingEmployeeId).
                    Single());
            }
        }

        [Test]
        public async Task GetDailyReports_RequestDailyReports_ReturnsCorrectReports()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                DateTime yesterday = DateTime.Today.AddDays(-1);
                DailyEmployeeReport firstEmployee = new DailyEmployeeReport
                {
                    Date = yesterday,
                    FirstName = "Shay",
                    LastName = "Musachanov",
                    NumberOfPatientsTreated = 4
                };
                DailyEmployeeReport secondEmployee = new DailyEmployeeReport
                {
                    Date = yesterday,
                    FirstName = "Daniel",
                    LastName = "Szuster",
                    NumberOfPatientsTreated = 3
                };
                DailyEmployeeReport thirdEmployee = new DailyEmployeeReport
                {
                    Date = yesterday,
                    FirstName = "David",
                    LastName = "Fineboym",
                    NumberOfPatientsTreated = 2
                };

                ActionResult<List<DailyEmployeeReport>> result = await controller.GetDailyReports(yesterday);

                Assert.IsInstanceOf<ActionResult<List<DailyEmployeeReport>>>(result);
                Assert.IsInstanceOf<List<DailyEmployeeReport>>(result.Value);
                List<DailyEmployeeReport> reports = result.Value;
                Assert.AreEqual(firstEmployee, reports.Find(e => e.FirstName == firstEmployee.FirstName));
                Assert.AreEqual(secondEmployee, reports.Find(e => e.FirstName == secondEmployee.FirstName));
                Assert.AreEqual(thirdEmployee, reports.Find(e => e.FirstName == thirdEmployee.FirstName));
            }
        }

        [Test]
        public async Task GetDailyReports_RequestReportsForNonexistentDate_ReturnsNotFound()
        {
            using (var context = GetInitializedUsersContext())
            {
                var usersService = new Services.UsersService(context, new QueueApiServiceMock());
                UsersController controller = new UsersController(usersService);
                DateTime nonExistentDate = DateTime.Today.AddDays(1);

                ActionResult<List<DailyEmployeeReport>> result = await controller.GetDailyReports(nonExistentDate);

                Assert.IsInstanceOf<ActionResult<List<DailyEmployeeReport>>>(result);
                Assert.IsInstanceOf<NotFoundObjectResult>(result.Result);
            }
        }
    }
}

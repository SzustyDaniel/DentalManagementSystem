using Common.UserModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UsersService.Data;
using UsersService.Data.Models;

namespace UsersService.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private static UsersContext GetUsersContext([CallerMemberName] string dbName = "testDB")
        {
            var options = new DbContextOptionsBuilder<UsersContext>().UseInMemoryDatabase(databaseName: dbName).Options;
            return new UsersContext(options);
        }

        private static QueueApiServiceMock GetQueueApiServiceMock()
        {
            return new QueueApiServiceMock();
        }

        [Test]
        public async Task GetCustomerIdentification_PassCardNumber_ReturnsCustomerId()
        {
            using (UsersContext context = GetUsersContext())
            {
                ulong cardId = 2;
                int expectedCustomerId = 1;
                context.Customers.Add(new Customer { CardNumber = cardId, CustomerId = expectedCustomerId });
                context.SaveChanges();

                Services.UsersService usersService = new Services.UsersService(context, GetQueueApiServiceMock());
                CustomerIdentification customerIdentification = await usersService.GetCustomerIdentification(cardId);
                int actualCustomerId = customerIdentification.CustomerId;

                Assert.AreEqual(expectedCustomerId, actualCustomerId);
            }
        }

        [Test]
        public async Task GetDailyEmployeeReports_RequestReport_ReturnsReports()
        {
            using (UsersContext context = GetUsersContext())
            {
                DateTime today = DateTime.Today;
                int employeeId = 10;
                string employeeFirstName = "First";
                string employeeLastName = "Last";
                context.Customers.AddRange(
                    new Customer { CustomerId = 1, CardNumber = 2 },
                    new Customer { CustomerId = 2, CardNumber = 3 });
                context.Employees.Add(new Employee { EmployeeId = employeeId, Firstname = employeeFirstName, Lastname = employeeLastName });
                context.Treatments.AddRange(
                    new Treatment { EmployeeId = employeeId, CustomerId = 1, TreatmentDate = today },
                    new Treatment { EmployeeId = employeeId, CustomerId = 2, TreatmentDate = today });
                context.SaveChanges();
                List<DailyEmployeeReport> expectedReports = new List<DailyEmployeeReport>
                {
                    new DailyEmployeeReport {Date = today, FirstName = employeeFirstName, LastName = employeeLastName, NumberOfPatientsTreated = 2 }
                };

                Services.UsersService usersService = new Services.UsersService(context, GetQueueApiServiceMock());
                List<DailyEmployeeReport> actualReports = await usersService.GetDailyEmployeeReports(today);

                Assert.AreEqual(expectedReports, actualReports);
            }
        }

        [Test]
        public async Task SaveCustomerTreatment_SaveOneTreatment_TreatmentInDb()
        {
            using (UsersContext context = GetUsersContext())
            {
                int employeeId = 11;
                int customerId = 1;
                DateTime today = DateTime.Today;
                Customer customer = new Customer { CustomerId = customerId, CardNumber = 100 };
                Employee employee = new Employee { EmployeeId = employeeId };
                context.Customers.Add(customer);
                context.Employees.Add(employee);
                context.SaveChanges();
                CustomerTreatment customerTreatment = new CustomerTreatment
                {
                    CustomerId = customerId,
                    DateOfTreatment = today,
                    TreatingEmployeeId = employeeId
                };
                Treatment expectedTreatment = new Treatment
                {
                    EmployeeId = employeeId,
                    CustomerId = customerId,
                    TreatmentDate = today,
                    Customer = customer,
                    Employee = employee
                };

                Services.UsersService usersService = new Services.UsersService(context, GetQueueApiServiceMock());
                await usersService.SaveCustomerTreatment(customerTreatment);

                Treatment actualTreatment = context.Treatments.Find(today, customerId, employeeId);
                Assert.AreEqual(expectedTreatment.TreatmentDate, actualTreatment.TreatmentDate);
                Assert.AreEqual(expectedTreatment.EmployeeId, actualTreatment.EmployeeId);
                Assert.AreEqual(expectedTreatment.CustomerId, actualTreatment.CustomerId);
            }
        }
    }
}

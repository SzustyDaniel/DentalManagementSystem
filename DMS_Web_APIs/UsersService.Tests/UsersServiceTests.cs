using Common.UserModels;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UsersService.Data;
using UsersService.Data.Models;

namespace UsersService.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private static UsersContext GetUsersContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<UsersContext>().UseInMemoryDatabase(databaseName: dbName).Options;
            return new UsersContext(options);
        }

        [Test]
        public async Task GetCustomerIdentification_PassCardNumber_ReturnsCustomerId()
        {
            using (UsersContext context = GetUsersContext(nameof(GetCustomerIdentification_PassCardNumber_ReturnsCustomerId)))
            {
                ulong cardId = 2;
                int expectedCustomerId = 1;
                context.Customers.Add(new Customer { CardNumber = cardId, CustomerId = expectedCustomerId });
                context.SaveChanges();
                Services.UsersService usersService = new Services.UsersService(context);

                CustomerIdentification customerIdentification = await usersService.GetCustomerIdentification(cardId);
                int actualCustomerId = customerIdentification.CustomerId;

                Assert.AreEqual(expectedCustomerId, actualCustomerId);
            }
        }
    }
}

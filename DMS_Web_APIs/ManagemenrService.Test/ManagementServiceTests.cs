using ManagementService.Data;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using Tests;

namespace ManagementService.Test
{
    [TestFixture]
    public class ManagementServiceTests
    {
        private static ManagementContext GetManagementContext([CallerMemberName] string dbName = "testDB")
        {
            var options = new DbContextOptionsBuilder<ManagementContext>().UseInMemoryDatabase(databaseName: dbName).Options;
            return new ManagementContext(options);
        }

        private static UsersApiServiceMock GetQueueApiServiceMock()
        {
            return new UsersApiServiceMock();
        }


        [Test]
        [TestCase(DayOfWeek.Sunday)]
        [TestCase(DayOfWeek.Monday)]
        public void GetScheduleAsync_TestIfAListIsReturned_AListWithScheduleIsReturned(DayOfWeek day)
        {
            using (var context = GetManagementContext())
            {
                // Arrange

                // Act

                // Assert
            }
        }
    }
}

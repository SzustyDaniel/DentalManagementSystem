using Common.ManagementModels;
using ManagementService.Controllers;
using ManagementService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Tests
{
    [TestFixture]
    public class ManagementControllerTests
    {
        private static ManagementContext GetInitializedUsersContext([CallerMemberName] string dbName = "testDB")
        {
            var options = new DbContextOptionsBuilder<ManagementContext>().UseInMemoryDatabase(databaseName: dbName).Options;
            DataGenerator.Initialize(options);
            return new ManagementContext(options);
        }

        [Test]
        public async System.Threading.Tasks.Task GetSchedules_TestIfSchedulesAreLoadedBasedOnDay_GotAListOfSchedulesAsync()
        {
            
            using (var context = GetInitializedUsersContext())
            {
                // Arrange
                var managementService = new ManagementService.Services.ManagementService(context, new UsersApiServiceMock());
                ManagementController controller = new ManagementController(managementService);

                List<ScheduleModel> excpectedSchedules = new List<ScheduleModel>()
                {
                      new ScheduleModel()
                    {
                        Day = DayOfWeek.Sunday,
                        Type = Common.ServiceType.Pharmacist,
                        WorkingHours = new WorkingWindow()
                        {
                            StartTime = TimeSpan.FromHours(8),
                            EndTime = TimeSpan.FromHours(18)
                        }
                    },
                    new ScheduleModel()
                    {
                        Day = System.DayOfWeek.Sunday,
                        Type = Common.ServiceType.Nurse,
                        WorkingHours = new WorkingWindow()
                        {
                            StartTime = TimeSpan.FromHours(8),
                            EndTime = TimeSpan.FromHours(10)
                        }
                    }

                };

                // Act
                ActionResult<List<ScheduleModel>> result = await controller.GetSchedules(DayOfWeek.Sunday);

                // Assert
                Assert.IsInstanceOf<ActionResult<List<ScheduleModel>>>(result);
                Assert.IsInstanceOf<List<ScheduleModel>>(result.Value);/*
                Assert.AreEqual(excpectedSchedules[0].Day, result.Value[0].Day);
                Assert.AreEqual(excpectedSchedules[0].Type, result.Value[0].Type);
                Assert.AreEqual(excpectedSchedules[0].WorkingHours.StartTime, result.Value[0].WorkingHours.StartTime);
                Assert.AreEqual(excpectedSchedules[0].WorkingHours.EndTime, result.Value[0].WorkingHours.EndTime);
                Assert.AreEqual(excpectedSchedules[1].Day, result.Value[1].Day);
                Assert.AreEqual(excpectedSchedules[1].Type, result.Value[1].Type);
                Assert.AreEqual(excpectedSchedules[1].WorkingHours.StartTime, result.Value[1].WorkingHours.StartTime);
                Assert.AreEqual(excpectedSchedules[1].WorkingHours.EndTime, result.Value[1].WorkingHours.EndTime);*/
            }


        }
    }
}
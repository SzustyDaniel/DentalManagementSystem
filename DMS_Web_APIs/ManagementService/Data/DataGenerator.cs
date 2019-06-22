using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Microsoft.EntityFrameworkCore;
using ManagementService.Data.Models;

namespace ManagementService.Data
{
    public class DataGenerator
    {
        public static void Initialize(DbContextOptions<ManagementContext> contextOptions)
        {
            using(var context = new ManagementContext(contextOptions))
            {
                context.Schedules.AddRange
                    (
                    new Schedule() { Day = DayOfWeek.Sunday, Service = ServiceType.Pharmacist, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(18)},
                    new Schedule() { Day = DayOfWeek.Monday, Service = ServiceType.Pharmacist, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(18) },
                    new Schedule() { Day = DayOfWeek.Tuesday, Service = ServiceType.Pharmacist, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(18) },
                    new Schedule() { Day = DayOfWeek.Wednesday, Service = ServiceType.Pharmacist, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(18) },
                    new Schedule() { Day = DayOfWeek.Thursday, Service = ServiceType.Pharmacist, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(18) },
                    new Schedule() { Day = DayOfWeek.Friday, Service = ServiceType.Pharmacist, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(12) },

                    new Schedule() { Day = DayOfWeek.Sunday, Service = ServiceType.Nurse, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(10) },
                    new Schedule() { Day = DayOfWeek.Monday, Service = ServiceType.Nurse, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(10) },
                    new Schedule() { Day = DayOfWeek.Tuesday, Service = ServiceType.Nurse, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(10) },
                    new Schedule() { Day = DayOfWeek.Wednesday, Service = ServiceType.Nurse, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(10) },
                    new Schedule() { Day = DayOfWeek.Thursday, Service = ServiceType.Nurse, Start = TimeSpan.FromHours(8), End = TimeSpan.FromHours(10) }
                    );

                context.SaveChanges();
            }
        }
    }
}

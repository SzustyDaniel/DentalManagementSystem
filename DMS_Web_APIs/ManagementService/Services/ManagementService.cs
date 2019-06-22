using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.ManagementModels;
using ManagementService.Data;
using ManagementService.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ManagementService.Services
{
    public class ManagementService : IManagementService
    {
        private readonly ManagementContext managementContext;

        public ManagementService(ManagementContext context)
        {
            managementContext = context;
        }

        public async Task<List<ScheduleModel>> GetScheduleAsync(DayOfWeek day)
        {
            var scheduleList = await managementContext.Schedules.Where(s => s.Day == day).ToListAsync();
            var returnedList = new List<ScheduleModel>();

            foreach (var item in scheduleList)
            {
                returnedList.Add(new ScheduleModel() { Type = item.Service, Day = item.Day, WorkingHours = new WorkingWindow() { StartTime = item.Start, EndTime = item.End } });
            }

            return returnedList;

        }
    }
}

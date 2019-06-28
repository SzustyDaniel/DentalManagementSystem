using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.ManagementModels;
using ManagementService.Data;
using ManagementService.Data.Models;
using Microsoft.EntityFrameworkCore;
using Common.UserModels;

namespace ManagementService.Services
{
    public class ManagementService : IManagementService
    {
        private readonly ManagementContext _managementContext;
        private readonly UsersApiService _usersApiService;

        public ManagementService(ManagementContext context, UsersApiService usersApiService)
        {
            _managementContext = context;
            _usersApiService = usersApiService;
        }

        /*
         * Method used for getting the schedule from the database of the server
         */
        public async Task<List<ScheduleModel>> GetScheduleAsync(DayOfWeek day)
        {
            var scheduleList = await _managementContext.Schedules.Where(s => s.Day == day).ToListAsync();
            var returnedList = new List<ScheduleModel>();

            foreach (var item in scheduleList)
            {
                returnedList.Add(new ScheduleModel() { Type = item.Service, Day = item.Day, WorkingHours = new WorkingWindow() { StartTime = item.Start, EndTime = item.End } });
            }

            return returnedList;

        }

        /*
         * Get treatments from the UsersApi service
         * return the treatment list
         */
        public async Task<List<DailyEmployeeReport>> GetCustomerTreatmentsAsync(DateTime date)
        {
            var treatmentList = await _usersApiService.GetUsersTtreatments(date);

            return treatmentList;
        }
    }
}

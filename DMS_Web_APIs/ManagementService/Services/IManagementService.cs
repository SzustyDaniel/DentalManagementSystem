using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Common.ManagementModels;
using Common.UserModels;
using ManagementService.Data.Models;

namespace ManagementService.Services
{
    public interface IManagementService
    {
        Task<List<ScheduleModel>> GetScheduleAsync(DayOfWeek day);
        Task<List<DailyEmployeeReport>> GetCustomerTreatmentsAsync(DateTime date);
    }
}

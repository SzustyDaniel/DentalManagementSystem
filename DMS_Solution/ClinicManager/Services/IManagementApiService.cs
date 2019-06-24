using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ClinicManager.Services
{
    public interface IManagementApiService
    {
        Task<IEnumerable<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date);
    }
}

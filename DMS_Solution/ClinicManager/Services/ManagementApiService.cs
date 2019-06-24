using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.UserModels;

namespace ClinicManager.Services
{
    public class ManagementApiService : IManagementApiService
    {
        private static readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri(ConstantURI.managementServerURI)
        };

        public async Task<IEnumerable<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date)
        {
            throw new NotImplementedException();
        }
    }
}

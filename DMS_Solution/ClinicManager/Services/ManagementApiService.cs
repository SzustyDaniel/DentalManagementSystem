using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Common;
using Common.UserModels;
using Newtonsoft.Json;

namespace ClinicManager.Services
{
    public class ManagementApiService : IManagementApiService
    {
        private const string GetDailyReportRoute = "management/reports?date={0}";
        private static readonly HttpClient HttpClient = new HttpClient
        {
            BaseAddress = new Uri(ConstantURI.managementServerURI)
        };

        public async Task<IList<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date)
        {
            string requestUri = string.Format(GetDailyReportRoute, date.ToString("s"));
            string result = await HttpClient.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<List<DailyEmployeeReport>>(result);
        }
    }
}

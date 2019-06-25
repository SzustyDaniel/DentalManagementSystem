using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Common;
using Common.UserModels;
using Newtonsoft.Json;

namespace ManagementService.Services
{
    public class UsersApiService
    {
        private readonly HttpClient client;

        public UsersApiService() { }

        public UsersApiService(HttpClient client)
        {
            this.client = client;
        }

        /* Get the customer Treatments from the users API */
        public virtual async Task<List<DailyEmployeeReport>> GetUsersTtreatments(DateTime date)
        {
            string requestUri = string.Format("{0}Users/reports?date={1}",ConstantURI.usersServerURI,date.ToString("s"));
            string result = await client.GetStringAsync(requestUri);
            return JsonConvert.DeserializeObject<List<DailyEmployeeReport>>(result);
        }

    }
}

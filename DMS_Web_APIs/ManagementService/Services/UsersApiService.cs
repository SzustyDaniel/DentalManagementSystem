using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Common;
using Common.UserModels;

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
        public virtual async Task<List<CustomerTreatment>> GetUsersTtreatments(DateTime date)
        {
            HttpResponseMessage result = await client.GetAsync($"{ConstantURI.usersServerURI}Users/reports?date={date.ToShortDateString()}");
            result.EnsureSuccessStatusCode();

            var treatmentList = await result.Content.ReadAsAsync<List<CustomerTreatment>>();

            return treatmentList;
        }

    }
}

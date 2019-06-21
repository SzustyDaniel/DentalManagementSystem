using Common.QueueModels;
using Common.UserModels;
using Common;
using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Services
{
    public class HttpActionsService : IHttpActions
    {
        private readonly HttpClient client;


        #region Singleton

        private static HttpActionsService _instance;
        public static HttpActionsService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HttpActionsService();
                }

                return _instance;
            }
        }

        private HttpActionsService()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        public Task<DequeuePositionResult> CallNextInQueue(DequeuePosition request)
        {
            throw new NotImplementedException();
        }

        public Task LogOut(EmployeeConnectionUpdate update)
        {
            throw new NotImplementedException();
        }

        public async Task SendCredentialsAsync(EmployeeLogin logAction)
        {
            HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"{ConstantURI.usersServerURI}Users/staff/authentication/login", logAction);
            httpResponse.EnsureSuccessStatusCode();
        }
    }
}

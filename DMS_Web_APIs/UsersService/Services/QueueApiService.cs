using Common;
using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace UsersService.Services
{
    public class QueueApiService
    {
        protected readonly HttpClient HttpClient;

        public QueueApiService()
        {
        }

        public QueueApiService(HttpClient client)
        {
            client.BaseAddress = new Uri(ConstantURI.queueServerURI);
            HttpClient = client;
        }

        // TODO : After Shay defines endpoint for posting, implement the following method.
        public virtual async Task PostUpdateOnUserLogin(EmployeeConnectionUpdate employeeConnectionUpdate)
        {

        }
    }
}

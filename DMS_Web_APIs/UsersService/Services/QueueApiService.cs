using Common;
using Common.UserModels;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace UsersService.Services
{
    public class QueueApiService
    {
        protected readonly HttpClient HttpClient;
        private static readonly string QueueControllerName = "queue";

        public QueueApiService()
        {
        }

        public QueueApiService(HttpClient client)
        {
            client.BaseAddress = new Uri(ConstantURI.queueServerURI);
            HttpClient = client;
        }

        public virtual async Task UpdateOnUserLogin(EmployeeConnectionUpdate employeeConnectionUpdate)
        {
            HttpResponseMessage result = await HttpClient.PutAsJsonAsync(QueueControllerName, employeeConnectionUpdate);
            result.EnsureSuccessStatusCode();
        }
    }
}

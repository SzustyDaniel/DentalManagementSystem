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
using System.IO;
using Newtonsoft.Json;

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

        /*
         * Call for the next client to the station
         */
        public async Task<DequeuePositionResult> CallNextInQueueAsync(DequeuePosition request)
        {
            HttpRequestMessage httpRequest = new HttpRequestMessage(HttpMethod.Delete, $"{ConstantURI.queueServerURI}Queue");
            httpRequest.Content = new StringContent(JsonConvert.SerializeObject(request),Encoding.UTF8,"application/json");
            var response = await client.SendAsync(httpRequest);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadAsAsync<DequeuePositionResult>();

            return result;
        }

        /*
         * Log-out the employee from the station
         */
        public async Task LogOutAsync(string userName)
        {

            HttpResponseMessage httpResponse = await client.PostAsync($"{ConstantURI.usersServerURI}Users/staff/authentication/{userName}/logout", null);
            httpResponse.EnsureSuccessStatusCode();
        }

        /*
         * Log-In the employee to the station and validate the credentials
         */
        public async Task<EmployeeInfo> SendCredentialsAsync(EmployeeLogin logAction)
        {

            HttpResponseMessage httpResponse = await client.PostAsJsonAsync($"{ConstantURI.usersServerURI}Users/staff/authentication/login", logAction);
            httpResponse.EnsureSuccessStatusCode();

            var employee = await httpResponse.Content.ReadAsAsync<EmployeeInfo>();
            return employee;

        }

        public async Task SendTreatmentReportAsync(CustomerTreatment treatment)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(treatment), Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"{ConstantURI.usersServerURI}Users/customers/history",content);
            response.EnsureSuccessStatusCode();

        }
    }
}

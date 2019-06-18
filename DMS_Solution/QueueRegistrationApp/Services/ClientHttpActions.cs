using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.QueueModels;
using Common.UserModels;

namespace QueueRegistrationApp.Services
{
    public class ClientHttpActions: IClientHttpActions
    {

        private readonly HttpClient client;
        private readonly string queueServerURI = "http://localhost:55034/";
        private readonly string usersServerURI = "http://localhost:/";

        #region Singleton

        private static ClientHttpActions _instance;
        public static ClientHttpActions Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ClientHttpActions();
                }

                return _instance;
            }
        }

        private ClientHttpActions()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        /*
         * Generate POST HTTP request to the API and return the queue position of the client.
         */
        public async Task<EnqueuePositionResult> RegisterToQueueAsync(EnqueuePosition requestPosition)
        {
            
            EnqueuePositionResult positionResult = null;

            client.BaseAddress = new Uri(queueServerURI);
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Queue",requestPosition);
            response.EnsureSuccessStatusCode();

            if(response.IsSuccessStatusCode)
            {
                positionResult = await response.Content.ReadAsAsync<EnqueuePositionResult>();
            }

            return positionResult;

        }

        public Task<CustomerRespone> ValidateCustomer(CardInfo cardInfo)
        {
            throw new NotImplementedException();
        }
    }
}

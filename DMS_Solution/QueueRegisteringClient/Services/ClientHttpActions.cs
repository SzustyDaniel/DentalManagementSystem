using Common.QueueModels;
using Common.UserModels;
using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace QueueRegisteringClient.Services
{
    public class ClientHttpActions : IClientHttpActions
    {

        private readonly HttpClient client;


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

            HttpResponseMessage response = await client.PostAsJsonAsync($"{ConstantURI.queueServerURI}Queue", requestPosition);
            response.EnsureSuccessStatusCode();

            positionResult = await response.Content.ReadAsAsync<EnqueuePositionResult>();
            return positionResult;
        }

        /*
         * Call the users api for the validation of the client card
         */
        public async Task<CustomerIdentification> ValidateCustomer(CardInfo cardInfo)
        {
            CustomerIdentification respone = new CustomerIdentification();
            
            HttpResponseMessage httpResponse = await client.GetAsync($"{ConstantURI.usersServerURI}Users/customers/authentication/{cardInfo.CardNumber.ToString()}");
            httpResponse.EnsureSuccessStatusCode();
            
            respone = await httpResponse.Content.ReadAsAsync<CustomerIdentification>();
            return respone;
        }
    }
}

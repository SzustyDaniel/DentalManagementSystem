using Common.QueueModels;
using Common.UserModels;
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
        private readonly string queueServerURI = "https://localhost:44305/";
        private readonly string usersServerURI = "http://localhost:53512/";

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
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Queue", requestPosition);
            response.EnsureSuccessStatusCode();

            if (response.IsSuccessStatusCode)
            {
                positionResult = await response.Content.ReadAsAsync<EnqueuePositionResult>();
            }

            return positionResult;

        }

        /*
         * Call the users api for the validation of the client card
         */
        public async Task<CustomerIdentification> ValidateCustomer(CardInfo cardInfo)
        {
            CustomerIdentification respone = new CustomerIdentification();

            client.BaseAddress = new Uri(usersServerURI);

            // current suggested code for the get request
            /*
            HttpContent content = new FormUrlEncodedContent(new Dictionary<string, string> { { "cardNumber", cardInfo.CardNumber.ToString() } });
            HttpRequestMessage message = new HttpRequestMessage(HttpMethod.Get, "Users/customers/authentication/")
            {
                Content = content
            };
            */


            HttpResponseMessage httpResponse = await client.GetAsync($"Users/customers/authentication/{cardInfo.CardNumber.ToString()}");
            // HttpResponseMessage httpResponse = await client.SendAsync(message);
            httpResponse.EnsureSuccessStatusCode();
            
            if (httpResponse.IsSuccessStatusCode)
            {
                respone = await httpResponse.Content.ReadAsAsync<CustomerIdentification>();
            }

            return respone;
        }
    }
}

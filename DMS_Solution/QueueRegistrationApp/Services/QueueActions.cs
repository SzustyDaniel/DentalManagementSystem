using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Common.QueueModels;

namespace QueueRegistrationApp.Services
{
    public class QueueActions
    {

        private readonly HttpClient client;
        private readonly string serverURI = "http://localhost:55034/";

        #region Singleton

        private static QueueActions _instance;
        public static QueueActions Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new QueueActions();
                }

                return _instance;
            }
        }

        private QueueActions()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(serverURI);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        #endregion

        /*
         * Generate POST HTTP request to the API and return the queue position of the client.
         */
        public async Task<EnqueuePositionResult> RegisterToQueueAsync(EnqueuePosition request)
        {
            EnqueuePositionResult positionResult = null;
            HttpResponseMessage response = await client.PostAsJsonAsync("api/Queue",request);
            response.EnsureSuccessStatusCode();

            if(response.IsSuccessStatusCode)
            {
                positionResult = await response.Content.ReadAsAsync<EnqueuePositionResult>();
            }

            return positionResult;

        }

    }
}

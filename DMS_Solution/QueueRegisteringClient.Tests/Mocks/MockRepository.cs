using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.UserModels;
using Common.QueueModels;

namespace QueueRegisteringClient.Tests.Mocks
{
    public class MockRepository
    {
        private Dictionary<CardInfo, CustomerIdentification> customerMockup;
        private EnqueuePositionResult positionResult;

        public MockRepository()
        {
            customerMockup = new Dictionary<CardInfo, CustomerIdentification>()
            {
                { new CardInfo() { CardNumber = 200}, new CustomerIdentification() { CustomerId = 1} },
                { new CardInfo() { CardNumber = 300}, new CustomerIdentification() { CustomerId = 2} },
                { new CardInfo() { CardNumber = 400}, new CustomerIdentification() { CustomerId = 3} },
                { new CardInfo() { CardNumber = 500}, new CustomerIdentification() { CustomerId = 4} },
                { new CardInfo() { CardNumber = 600}, new CustomerIdentification() { CustomerId = 5} },
                { new CardInfo() { CardNumber = 700}, new CustomerIdentification() { CustomerId = 6} },
                { new CardInfo() { CardNumber = 800}, new CustomerIdentification() { CustomerId = 7} },
                { new CardInfo() { CardNumber = 900}, new CustomerIdentification() { CustomerId = 8} },
                { new CardInfo() { CardNumber = 1000}, new CustomerIdentification() { CustomerId = 9} }
            };

            positionResult = new EnqueuePositionResult() { UserNumber = 1 };
        }

        internal async Task<CustomerIdentification> GetClientId(CardInfo cardInfo)
        {
            return await Task.FromResult(customerMockup[cardInfo]);
        }

        internal async Task<EnqueuePositionResult> GetEnqueuePositionResult(EnqueuePosition requestPosition)
        {
            var task = await Task.FromResult(positionResult);
            positionResult.UserNumber++;

            return task;
        }
    }
}
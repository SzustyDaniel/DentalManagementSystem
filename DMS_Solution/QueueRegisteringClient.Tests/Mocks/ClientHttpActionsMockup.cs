using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QueueRegisteringClient.Services;
using Common;
using Common.QueueModels;
using Common.UserModels;
using Common.ManagementModels;

namespace QueueRegisteringClient.Tests.Mocks
{
    public class ClientHttpActionsMockup : IClientHttpActions
    {
        private MockRepository repository = new MockRepository();

        public async Task<List<ScheduleModel>> GetSchedulesAsync(DayOfWeek day)
        {
            return await repository.GetSchedules(day);
        }

        public async Task<EnqueuePositionResult> RegisterToQueueAsync(EnqueuePosition requestPosition)
        {
            return await repository.GetEnqueuePositionResult(requestPosition);
        }

        public async Task<CustomerIdentification> ValidateCustomerAsync(CardInfo cardInfo)
        {
            return await repository.GetClientId(cardInfo);
        }
    }
}

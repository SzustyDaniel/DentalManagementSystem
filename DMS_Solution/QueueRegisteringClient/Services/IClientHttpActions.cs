using Common.QueueModels;
using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueRegisteringClient.Services
{
    public interface IClientHttpActions
    {
        Task<EnqueuePositionResult> RegisterToQueueAsync(EnqueuePosition requestPosition);
        Task<CustomerIdentification> ValidateCustomerAsync(CardInfo cardInfo);
    }
}

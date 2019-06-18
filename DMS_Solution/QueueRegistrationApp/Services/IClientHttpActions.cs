using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.QueueModels;
using Common.UserModels;

namespace QueueRegistrationApp.Services
{
    public interface IClientHttpActions
    {
        Task<EnqueuePositionResult> RegisterToQueueAsync(EnqueuePosition requestPosition);
        Task<CustomerRespone> ValidateCustomer(CardInfo cardInfo);
    }
}

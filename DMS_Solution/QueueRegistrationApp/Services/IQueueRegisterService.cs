using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.QueueModels;

namespace QueueRegistrationApp.Services
{
    public interface IQueueRegisterService
    {
        Task<EnqueuePositionResult> RegisterToQueueAsync(EnqueuePosition requestPosition);
    }
}

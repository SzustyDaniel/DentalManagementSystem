using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.UserModels;

namespace QueueRegistrationApp.Services
{
    public interface IClientActionsService
    {
        Task<CustomerRespone> IdentifyCustomer(CardInfo card);
    }
}

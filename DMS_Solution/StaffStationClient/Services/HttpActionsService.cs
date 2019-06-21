using Common.QueueModels;
using Common.UserModels;
using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Services
{
    public class HttpActionsService : IHttpActions
    {
        private static readonly HttpClient client = new HttpClient();

        public Task<DequeuePositionResult> CallNextInQueue(DequeuePosition request)
        {
            throw new NotImplementedException();
        }

        public Task LogOut(EmployeeConnectionUpdate update)
        {
            throw new NotImplementedException();
        }

        public Task<LoginStatus> SendCredentials(EmployeeLogin logAction)
        {
            throw new NotImplementedException();
        }
    }
}

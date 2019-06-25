using StaffStationClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.QueueModels;
using Common.UserModels;

namespace StaffStationClient.Tests.Mocks
{
    class ClientHttpActionsMockup : IHttpActions
    {
        private MockRepository mockRepository = MockRepository.Instance;

        public async Task<DequeuePositionResult> CallNextInQueueAsync(DequeuePosition request)
        {
            DequeuePositionResult result;

            switch (request.ServiceType)
            {
                case ServiceType.none:
                    throw new ArgumentException("Argument is invalid");
                case ServiceType.Pharmacist:
                    result = await mockRepository.GetDequeuePositionPharmacy(request);
                    break;
                case ServiceType.Nurse:
                    result = await mockRepository.GetDequeuePositionNurse(request);
                    break;
                default:
                    throw new ArgumentException("Argument is not set");
            }

            return result;
        }

        public Task LogOutAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<EmployeeInfo> SendCredentialsAsync(EmployeeLogin logAction)
        {
            return await mockRepository.Login(logAction);
        }

        public async Task SendTreatmentReportAsync(CustomerTreatment treatment)
        {
            await mockRepository.ReportTretment(treatment);
        }
    }
}

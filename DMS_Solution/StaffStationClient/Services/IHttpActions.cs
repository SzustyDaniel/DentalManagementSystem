using Common.UserModels;
using Common.QueueModels;
using StaffStationClient.Models;
using System.Threading.Tasks;

namespace StaffStationClient.Services
{
    public interface IHttpActions
    {
        Task<EmployeeInfo> SendCredentialsAsync(EmployeeLogin logAction);
        Task LogOutAsync(string  userName);
        Task<DequeuePositionResult> CallNextInQueueAsync(DequeuePosition request);
    }
}
using Common.UserModels;
using Common.QueueModels;
using StaffStationClient.Models;
using System.Threading.Tasks;

namespace StaffStationClient.Services
{
    public interface IHttpActions
    {
        Task SendCredentialsAsync(EmployeeLogin logAction);
        Task LogOutAsync(string  userName);
        Task<DequeuePositionResult> CallNextInQueue(DequeuePosition request);
    }
}
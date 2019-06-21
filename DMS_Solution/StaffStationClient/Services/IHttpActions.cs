using Common.UserModels;
using Common.QueueModels;
using StaffStationClient.Models;
using System.Threading.Tasks;

namespace StaffStationClient.Services
{
    public interface IHttpActions
    {
        Task<LoginStatus> SendCredentials(EmployeeLogin logAction);
        Task LogOut(EmployeeConnectionUpdate update);
        Task<DequeuePositionResult> CallNextInQueue(DequeuePosition request);
    }
}
using Common.UserModels;
using System.Threading.Tasks;
using UsersService.Services;

namespace UsersService.Tests
{
    public class QueueApiServiceMock : QueueApiService
    {
        public override Task UpdateOnUserLogin(EmployeeConnectionUpdate employeeConnectionUpdate)
        {
            return Task.CompletedTask;
        }
    }
}

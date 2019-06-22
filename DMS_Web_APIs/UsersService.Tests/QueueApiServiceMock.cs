using Common.UserModels;
using System.Threading.Tasks;
using UsersService.Services;

namespace UsersService.Tests
{
    public class QueueApiServiceMock : QueueApiService
    {
        public override Task PostUpdateOnUserLogin(EmployeeConnectionUpdate employeeConnectionUpdate)
        {
            return Task.CompletedTask;
        }
    }
}

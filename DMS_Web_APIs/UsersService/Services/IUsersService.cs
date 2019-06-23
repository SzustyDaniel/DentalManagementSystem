using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsersService.Services
{
    public interface IUsersService
    {
        Task<CustomerIdentification> GetCustomerIdentification(ulong cardId);
        Task SaveCustomerTreatment(CustomerTreatment customerTreatment);
        Task<List<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date);
        Task<(bool isLoginSuccessful, EmployeeInfo employeeInfo)> TryLoginEmployee(EmployeeLogin employeeLogin);
        Task LogoutEmployee(string userName);
    }
}

using Common.UserModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsersService.Services
{
    public interface IUsersService
    {
        Task<CustomerIdentification> GetCustomerIdentification(ulong cardId);
        Task SaveCustomerTreatment(CustomerTreatment customerTreatment);
        Task<List<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date);
    }
}

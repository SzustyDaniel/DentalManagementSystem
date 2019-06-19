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
        Task<Dictionary<DateTime, List<DailyEmployeeReport>>> GetDailyEmployeeReports(DateTime fromDate, DateTime toDate);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.UserModels;
using UsersService.Data;

namespace UsersService.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersContext _usersContext;

        public UsersService(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public Task<CustomerIdentification> GetCustomerIdentification(ulong cardId)
        {
            throw new NotImplementedException();
        }

        public Task<Dictionary<DateTime, List<DailyEmployeeReport>>> GetDailyEmployeeReports(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}

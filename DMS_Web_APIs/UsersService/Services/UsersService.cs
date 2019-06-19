using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.UserModels;
using Microsoft.EntityFrameworkCore;
using UsersService.Data;
using UsersService.Data.Models;

namespace UsersService.Services
{
    public class UsersService : IUsersService
    {
        private readonly UsersContext _usersContext;

        public UsersService(UsersContext usersContext)
        {
            _usersContext = usersContext;
        }

        public async Task<CustomerIdentification> GetCustomerIdentification(ulong cardId)
        {
            Customer customer = await _usersContext.Customers.Where(c => c.CardNumber == cardId).SingleOrDefaultAsync();
            CustomerIdentification customerIdentification = new CustomerIdentification();
            if (customer != null)
                customerIdentification.CustomerId = customer.CustomerId;
            return customerIdentification;
        }

        public async Task<Dictionary<DateTime, List<DailyEmployeeReport>>> GetDailyEmployeeReports(DateTime fromDate, DateTime toDate)
        {
            throw new NotImplementedException();
        }
    }
}

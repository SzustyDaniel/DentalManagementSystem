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

        public async Task<List<DailyEmployeeReport>> GetDailyEmployeeReports(DateTime date)
        {
            List<DailyEmployeeReport> reports = new List<DailyEmployeeReport>();
            List<Employee> employees = await _usersContext.Employees.Include(e => e.Treatments).ToListAsync();
            foreach (Employee employee in employees)
            {
                employee.Treatments = employee.Treatments.Where(t => t.TreatmentDate == date.Date).ToArray();
                DailyEmployeeReport report = new DailyEmployeeReport
                {
                    Date = date,
                    FirstName = employee.Firstname,
                    LastName = employee.Lastname,
                    NumberOfPatientsTreated = employee.Treatments.Count
                };
                reports.Add(report);
            }
            return reports;
        }

        public async Task SaveCustomerTreatment(CustomerTreatment customerTreatment)
        {
            Treatment treatment = new Treatment
            {
                CustomerId = customerTreatment.CustomerId,
                EmployeeId = customerTreatment.TreatingEmployeeId,
                TreatmentDate = customerTreatment.DateOfTreatment.Date
            };
            _usersContext.Treatments.Add(treatment);
            await _usersContext.SaveChangesAsync();
        }
    }
}

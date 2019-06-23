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
        private readonly QueueApiService _queueApiService;

        public UsersService(UsersContext usersContext, QueueApiService queueApiService)
        {
            _usersContext = usersContext;
            _queueApiService = queueApiService;
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

        public async Task LogoutEmployee(string userName)
        {
            Employee employee = await _usersContext.Employees.Where(e => e.Username == userName).SingleAsync();
            employee.Online = false;
            Task<int> saveChangesTask = _usersContext.SaveChangesAsync();
            EmployeeConnectionUpdate update = new EmployeeConnectionUpdate
            {
                LoginStatus = LoginStatus.LogOut,
                ServiceType = employee.Role,
                StationNumber = employee.StationId
            };
            Task updateQueueApiTask = _queueApiService.PostUpdateOnUserLogin(update);
            await Task.WhenAll(saveChangesTask, updateQueueApiTask);
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

        public async Task<(bool isLoginSuccessful, EmployeeInfo employeeInfo)> TryLoginEmployee(EmployeeLogin employeeLogin)
        {
            Employee employee = await _usersContext.Employees.Where(e => e.Username == employeeLogin.Username).SingleAsync();
            EmployeeInfo employeeInfo = null;
            if (employee.Role != employeeLogin.ServiceType)
                throw new InvalidOperationException("Received employee service type does not match his service type in DB.");

            if (employee.Password != employeeLogin.Password || employee.Online)
                return (false, employeeInfo);

            employee.Online = true;
            employee.StationId = employeeLogin.StationNumber;
            Task<int> saveChangesTask = _usersContext.SaveChangesAsync();
            EmployeeConnectionUpdate update = new EmployeeConnectionUpdate
            {
                LoginStatus = LoginStatus.LogIn,
                StationNumber = employee.StationId,
                ServiceType = employee.Role
            };
            Task updateQueueApiTask = _queueApiService.PostUpdateOnUserLogin(update);
            employeeInfo = new EmployeeInfo
            {
                EmployeeId = employee.EmployeeId,
                Firstname = employee.Firstname,
                Lastname = employee.Lastname
            };
            await Task.WhenAll(saveChangesTask, updateQueueApiTask);

            return (true, employeeInfo);
        }
    }
}

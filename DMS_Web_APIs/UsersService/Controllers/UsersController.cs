﻿using System;
using System.Threading.Tasks;
using Common.UserModels;
using Microsoft.AspNetCore.Mvc;
using UsersService.Services;

namespace UsersService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        [HttpGet("/customers/authentication/{cardNumber}")]
        public async Task<IActionResult> GetCustomerNumber(ulong cardNumber)
        {
            // TODO : Delegate to IUsersService, it'll check if cardNumber exists and return class CustomerRespone
            throw new NotImplementedException();
        }

        [HttpPut("/staff/authentication")]
        public async Task<IActionResult> PutEmployeeLogAction([FromBody] EmployeeLogAction employeeLogAction)
        {
            // TODO : Delegate to IUsersService, it'll check if action is log in or log out. If log in, then check if already online. 
            // On either action update Queue API.
            throw new NotImplementedException();
        }

        [HttpPost("/customers/{customerId}/history")]
        public async Task<IActionResult> PostCustomerTreatment(int customerId, [FromBody] CustomerTreatment customerTreatment)
        {
            if (customerId != customerTreatment.CustomerId)
                return Conflict("Customer IDs do not match.");
            // TODO : Delegate to IUsersService, it'll update the DB with customerTreatment.
            throw new NotImplementedException();
        }

        [HttpGet("/reports")]
        public async Task<IActionResult> GetDailyReports([FromQuery(Name = "fromDate")] DateTime fromDate, [FromQuery(Name = "toDate")] DateTime toDate)
        {
            // TODO : Delegate to IUsersService, it'll return Dictionary< Key = Date, Value = List<DailyEmployeeReport> >
            throw new NotImplementedException();
        }
    }
}
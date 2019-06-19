using System;
using System.Collections.Generic;
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
            try
            {
                CustomerIdentification customer = await _usersService.GetCustomerIdentification(cardNumber);
                if (customer.CustomerId == default)
                    return NotFound();
                return Ok(customer);
            }
            catch (Exception)
            {
                return BadRequest();
            }
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
                return BadRequest("Customer IDs do not match.");
            // TODO : Delegate to IUsersService, it'll update the DB with customerTreatment.
            throw new NotImplementedException();
        }

        [HttpGet("/reports")]
        public async Task<IActionResult> GetDailyReports([FromQuery(Name = "fromDate")] DateTime fromDate, [FromQuery(Name = "toDate")] DateTime toDate)
        {
            try
            {
                if (fromDate > toDate)
                    return BadRequest("fromDate is larger than toDate");

                Dictionary<DateTime, List<DailyEmployeeReport>> dailyReports = 
                     await _usersService.GetDailyEmployeeReports(fromDate, toDate);

                if (dailyReports == null)
                    return NoContent();

                return Ok(dailyReports);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}

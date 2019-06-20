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

        [HttpGet("customers/authentication/{cardNumber}")]
        public async Task<IActionResult> GetCustomerNumber(ulong cardNumber)
        {
            try
            {
                CustomerIdentification customer = await _usersService.GetCustomerIdentification(cardNumber);
                if (customer.CustomerId == default)
                    return NotFound();
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }

        [HttpPut("staff/authentication")]
        public async Task<IActionResult> PutEmployeeLogAction([FromBody] EmployeeLogAction employeeLogAction)
        {
            // TODO : Delegate to IUsersService, it'll check if action is log in or log out. If log in, then check if already online. 
            // On either action update Queue API.
            throw new NotImplementedException();
        }

        [HttpPost("customers/{customerId}/history")]
        public async Task<IActionResult> PostCustomerTreatment(int customerId, [FromBody] CustomerTreatment customerTreatment)
        {
            if (customerId != customerTreatment.CustomerId)
                return BadRequest("Customer IDs do not match.");

            try
            {
                await _usersService.SaveCustomerTreatment(customerTreatment);
                return CreatedAtAction(nameof(GetDailyReports), customerTreatment);
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }

        [HttpGet("reports")]
        public async Task<IActionResult> GetDailyReports([FromQuery(Name = "date")] DateTime date)
        {
            try
            {
                List<DailyEmployeeReport> dailyReports = await _usersService.GetDailyEmployeeReports(date);

                if (dailyReports == null)
                    return NoContent();

                return Ok(dailyReports);
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }
    }
}

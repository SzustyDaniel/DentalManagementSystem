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
        public async Task<ActionResult<CustomerIdentification>> GetCustomerId(ulong cardNumber)
        {
            try
            {
                CustomerIdentification customer = await _usersService.GetCustomerIdentification(cardNumber);
                if (customer.CustomerId == default)
                    return NotFound(cardNumber);
                return customer;
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }

        [HttpPost("staff/authentication/login")]
        public async Task<ActionResult<EmployeeInfo>> PatchEmployeeLogin([FromBody] EmployeeLogin employeeLogin)
        {
            try
            {
                (bool isLoginSuccessful, EmployeeInfo employeeInfo) = await _usersService.TryLoginEmployee(employeeLogin);
                if (!isLoginSuccessful)
                    return Unauthorized(employeeLogin);

                return employeeInfo;
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }

        [HttpPost("staff/authentication/{userName}/logout")]
        public async Task<IActionResult> PatchEmployeeLogout(string userName)
        {
            try
            {
                await _usersService.LogoutEmployee(userName);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }

        [HttpPost("customers/history")]
        public async Task<IActionResult> PostCustomerTreatment([FromBody] CustomerTreatment customerTreatment)
        {
            try
            {
                await _usersService.SaveCustomerTreatment(customerTreatment);
                return CreatedAtAction(nameof(PostCustomerTreatment), customerTreatment);
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
                    return NotFound(date);

                return Ok(dailyReports);
            }
            catch (Exception e)
            {
                return BadRequest(e); // Not best practice to return any exception data, but we don't have and don't need loggers.
            }
        }
    }
}

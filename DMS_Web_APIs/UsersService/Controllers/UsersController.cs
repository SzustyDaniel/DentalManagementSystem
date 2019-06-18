using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IActionResult> PutEmployeeLogin([FromBody] EmployeeLogAction employeeLogAction)
        {
            // TODO : Delegate to IUsersService, it'll check if action is log in or log out. If log in, then check if already online. 
            // On either action update Queue API.
            throw new NotImplementedException();
        }
    }
}

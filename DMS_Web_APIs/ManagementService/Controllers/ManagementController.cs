using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementService.Data;
using ManagementService.Services;
using Microsoft.AspNetCore.Mvc;
using Common;

namespace ManagementService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ManagementContext contextInstance;
        private readonly IManagementService managementService;

        public ManagementController(ManagementContext context, IManagementService service)
        {
            contextInstance = context;
            managementService = service;
        }

        [HttpGet("Schedules/{day}")]
        public async Task<IActionResult> GetSchedules(DayOfWeek day)
        {
            if (DayOfWeek.Saturday == day)
                return NoContent();

            var result = await managementService.GetScheduleAsync(day);
            if(result != null)
                return Ok(result);

            return NotFound();
        }
    
    }
}

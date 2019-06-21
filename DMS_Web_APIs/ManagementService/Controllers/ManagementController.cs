using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementService.Data;
using Microsoft.AspNetCore.Mvc;

namespace ManagementService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly ManagementContext contextInstance;

        public ManagementController(ManagementContext context)
        {
            contextInstance = context;
        }

        [HttpGet("Schedules")]
        public IActionResult GetSchedules()
        {
            var result = contextInstance.Schedules.ToList();

            return Ok(result);
        }
    
    }
}

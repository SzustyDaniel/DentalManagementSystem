using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ManagementService.Data;
using ManagementService.Services;
using Microsoft.AspNetCore.Mvc;
using Common;
using Common.UserModels;
using Common.ManagementModels;

namespace ManagementService.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ManagementController : ControllerBase
    {
        private readonly IManagementService managementService;

        public ManagementController(IManagementService service)
        {
            managementService = service;
        }

        [HttpGet("Schedules/{day}")]
        public async Task<ActionResult<List<ScheduleModel>>> GetSchedules(DayOfWeek day)
        {
            if (DayOfWeek.Saturday == day)
                return NoContent();

            var result = await managementService.GetScheduleAsync(day);
            if(result != null)
                return result;

            return NotFound();
        }
        

        [HttpGet("reports")]
        public async Task<ActionResult<List<DailyEmployeeReport>>> GetTreatments([FromQuery] DateTime date)
        {

            if (date == null)
                return BadRequest();

            var treatments = await managementService.GetCustomerTreatmentsAsync(date);

            if (treatments == null)
                return NotFound();

            return treatments;
        }
    
    }
}

using System;
using System.Threading.Tasks;
using Common;
using Common.QueueModels;
using Common.UserModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QueueService.AzureStorage;
using QueueService.SignalR;

namespace QueueService.Controller
{
    [Route("[controller]")]
    public class QueueController : ControllerBase
    {
        private readonly IQueueStorageService _queueService;
        private readonly IHubContext<QueueNotificationsHub, IQueueNotificationsHub> _hubContext;

        public QueueController(IHubContext<QueueNotificationsHub, IQueueNotificationsHub> hubContext, IQueueStorageService queueService)
        {
            _queueService = queueService;
            _hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] EnqueuePosition item)
        {
            if(item is null || item.ServiceType == ServiceType.none)
            {
                return new BadRequestResult();
            }

            EnqueuePositionResult result = await _queueService.AddToQueue(item);

            if(result is null)
            {
                return new StatusCodeResult(500);
            }
            return new JsonResult(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove([FromBody] DequeuePosition item)
        {
            if (item is null || item.ServiceType == ServiceType.none)
            {
                return new BadRequestResult();
            }

            DequeuePositionResult result = await _queueService.RemoveFromQueue(item);
            if(result is null)
            {
                return new NoContentResult();
            }

            var notification = new QueueNotification
            {
                StationNumber = item.StationNumber,
                UserNumber = result.CustomerNumberInQueue
            };
            try
            {
                string groupName = item.ServiceType.ToString();
                await _hubContext.Clients.Group(groupName).SendQueueNotificationToGroup(notification);
            }
            catch (Exception)
            {

            }

            return new OkObjectResult(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateStationState([FromBody] EmployeeConnectionUpdate update)
        {
            if(update.ServiceType == ServiceType.none)
            {
                return new BadRequestObjectResult("ServiceType is none");
            }

            IQueueNotificationsHub group = _hubContext.Clients.Groups(update.ServiceType.ToString());

            if(group == null)
            {
                return new BadRequestObjectResult($"group for {update.ServiceType.ToString()} not found");
            }

            if (update.LoginStatus == LoginStatus.None)
            {
                return new BadRequestObjectResult("LoginStatus is none");
            }

            try
            {
                switch (update.LoginStatus)
                {
                    case LoginStatus.LogIn:
                        await group.AddStation(update.StationNumber);
                        break;
                    case LoginStatus.LogOut:
                        await group.RemoveStation(update.StationNumber);
                        break;
                }
            }
            catch(Exception)
            {
                
            }

            return new OkResult();
        }
    }
}
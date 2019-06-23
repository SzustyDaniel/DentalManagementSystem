using System;
using System.Threading.Tasks;
using Common;
using Common.QueueModels;
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
    }
}
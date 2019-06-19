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
        //IHubContext<QueueNotificationsHub, IQueueNotificationsHub> hubContext, 
        //private readonly IHubContext<QueueNotificationsHub, IQueueNotificationsHub> _hubContext;
        private readonly IQueueStorageService _queueService;

        public QueueController(IQueueStorageService queueService)
        {
            _queueService = queueService;
            //_hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddToTable([FromBody] EnqueuePosition item)
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
        public async Task<IActionResult> RemoveFromQueue([FromBody] DequeuePosition item)
        {
            if (item is null || item.ServiceType == ServiceType.none)
            {
                return new BadRequestResult();
            }

            DequeuePositionResult result = await _queueService.RemoveFromQueue(item);

            if(result is null) { }

            return new OkObjectResult(result);
        }


    }
}
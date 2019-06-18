using System.Threading.Tasks;
using Common;
using Common.QueueModels;
using Microsoft.AspNetCore.Mvc;
using QueueService.AzureStorage;

namespace QueueService.Controller
{
    [Route("api/[controller]")]
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
        public async Task<IActionResult> AddToTable([FromBody]EnqueuePosition newItem)
        {
            if(newItem is null || newItem.ServiceType == ServiceType.none)
            {
                return new BadRequestResult();
            }

            EnqueuePositionResult result = await _queueService.AddToQueue(newItem);

            if(result is null)
            {
                return new StatusCodeResult(500);
            }
            return new JsonResult(result);
        }
    }
}
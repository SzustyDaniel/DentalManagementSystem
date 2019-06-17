using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common.QueueModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using QueueService.AzureStorage;
using QueueService.AzureStorage.QueueManagement;
using QueueService.SignalR;

namespace QueueService.Controller
{
    [Route("api/[controller]")]
    public class QueueController : ControllerBase
    {
        //IHubContext<QueueNotificationsHub, IQueueNotificationsHub> hubContext, 
        //private readonly IHubContext<QueueNotificationsHub, IQueueNotificationsHub> _hubContext;
        private readonly IAzureQueueService _queueService;

        public QueueController(IAzureQueueService queueService)
        {
            _queueService = queueService;
            //_hubContext = hubContext;
        }

        [HttpPost]
        public async Task<IActionResult> AddToQueue(EnqueuePosition newItem)
        {
            if(newItem is null)
            {

            }

            EnqueuePositionResult result = await _queueService.AddToQueue(newItem);

            if(result is null)
            {
                return null;
            }
            return new JsonResult(result);
        }
    }
}
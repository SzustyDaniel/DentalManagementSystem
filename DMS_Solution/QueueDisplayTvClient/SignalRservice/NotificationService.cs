using Common.QueueModels;
using Microsoft.AspNetCore.SignalR.Client;
using QueueDisplayTvClient.Models;
using QueueDisplayTvClient.SignalRservice.Notification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDisplayTvClient.SignalRservice
{
    public static class NotificationServiceBuilder
    {
        public static async Task Builder(INotificationHandler handlers)
        {
            HubConnection connection = new HubConnectionBuilder().WithUrl("https://localhost:44305/QueueNotificationsHub").Build();
            connection.Closed += async (error) =>
            {
                await Task.Delay(1000);
                await connection.StartAsync();
            };

            connection.On<QueueNotification>("SendQueueNotificationToGroup", handlers.UpdateStation);
            connection.On<int>("RemoveStation", handlers.RemoveStation);
            connection.On<int>("AddStation", handlers.AddStation);

            try
            {
                await connection.StartAsync();
            }
            catch (Exception e)
            {

            }

            try
            {
                await connection.InvokeAsync("AddToGroup", handlers.QueueType);
            }
            catch (Exception e)
            {

            }
        }
    }
}

using Common.QueueModels;
using QueueDisplayTvClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDisplayTvClient.SignalRservice.Notification
{
    public interface INotificationHandler
    {
        ObservableCollection<Station> Stations { get; set; }

        string QueueType { get; set; }

        void AddStation(int station);

        void RemoveStation(int station);

        void UpdateStation(QueueNotification notification);
    }
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.QueueModels;
using QueueDisplayTvClient.Models;

namespace QueueDisplayTvClient.SignalRservice.Notification
{
    public class NotificationHandler : INotificationHandler
    {
        public ObservableCollection<Station> Stations { get; set; }
        public string QueueType { get; set; }

        public void AddStation(int station)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var newStation = Stations.FirstOrDefault(s => s.StationNumber == station);
                if (newStation is null)
                {
                    Stations.Add(new Station { StationNumber = station, PatientNumber = -1 });
                }
            });
        }

        public void RemoveStation(int station)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var StationToRemove = Stations.Where(s => s.StationNumber == station).FirstOrDefault();
                if (StationToRemove != null)
                {
                    Stations.Remove(StationToRemove);
                }
            });
        }

        public void UpdateStation(QueueNotification notification)
        {
            App.Current.Dispatcher.Invoke(() =>
            {
                var station = Stations.Where(s => s.StationNumber == notification.StationNumber).FirstOrDefault();
                if (station != null)
                {
                    station.PatientNumber = notification.UserNumber;
                }
            });
        }
    }
}
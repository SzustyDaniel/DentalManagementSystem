using Common;
using Common.QueueModels;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using QueueDisplayTvClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Threading;

namespace QueueDisplayTvClient.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        #region priv
        private HubConnection connection;
        private object _lock;
        #endregion

        #region pro
        private string _title = "Queue display";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private string _queueType;
        public string QueueType
        {
            get { return _queueType; }
            set { SetProperty(ref _queueType, value); }
        }
        #endregion

        #region status
        private ObservableCollection<Station> _items = new ObservableCollection<Station>();
        public ObservableCollection<Station> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        #endregion

        #region command
        private DelegateCommand _windowLoaded;
        public DelegateCommand WindowLoaded => _windowLoaded ?? (_windowLoaded = new DelegateCommand(WindowLoadedCommand));

        #endregion

        public MainWindowViewModel()
        {
            QueueType = ServiceType.Nurse.ToString();
            _lock = new object();
            WindowLoaded.Execute();
        }

        
        private async void WindowLoadedCommand()
        {
            connection = new HubConnectionBuilder().WithUrl("https://localhost:44305/QueueNotificationsHub").Build();

            connection.Closed += async (error) =>
            {
                await Task.Delay(1000);
                await connection.StartAsync();
            };

            connection.On<QueueNotification>("SendQueueNotificationToGroup", (notification) =>
            {
                App.Current.Dispatcher.Invoke(() =>
                {
                    var station = Items.Where(s => s.StationNumber == notification.StationNumber).FirstOrDefault();

                    if (station != null)
                    {
                        Items.Remove(station);
                    }

                    station = new Station { StationNumber = notification.StationNumber, PatientNumber = notification.UserNumber };
                    Items.Add(station);
                    Items = new ObservableCollection<Station>(Items.OrderBy(s => s.StationNumber));
                });
            });

            try
            {
                await connection.StartAsync();
            }
            catch (Exception e)
            {

            }

            try
            {
                await connection.InvokeAsync("AddToGroup", QueueType);
            }
            catch (Exception e)
            {

            }
        }

    }
}

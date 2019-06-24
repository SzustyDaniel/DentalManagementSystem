using Common;
using Common.QueueModels;
using Microsoft.AspNetCore.SignalR.Client;
using Prism.Commands;
using Prism.Mvvm;
using QueueDisplayTvClient.Models;
using QueueDisplayTvClient.SignalRservice;
using QueueDisplayTvClient.SignalRservice.Notification;
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

        private ObservableCollection<Station> _items;
        public ObservableCollection<Station> Items
        {
            get { return _items; }
            set { SetProperty(ref _items, value); }
        }
        #endregion

        public MainWindowViewModel()
        {
            Items = new ObservableCollection<Station>();
            NotificationServiceBuilder.Builder(
                new NotificationHandler()
                {
                    Stations = Items,
                    QueueType = ServiceType.Nurse.ToString()
                }).ConfigureAwait(false);
        }
    }
}

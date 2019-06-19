using Common;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Models
{
    public class StationModel : BindableBase
    {

        private string _userName;
        public string UserName
        {
            get { return _userName; }
            set { SetProperty(ref _userName, value); }
        }

        private string _password;
        public string Password
        {
            get { return _password; }
            set { SetProperty(ref _password, value); }
        }

        private ServiceType serviceType;
        public ServiceType StationServiceType
        {
            get { return serviceType; }
            set { SetProperty(ref serviceType, value); }
        }

        private int stationNumber;
        public int StationNumber
        {
            get { return stationNumber; }
            set { SetProperty(ref stationNumber, value); }
        }

    }
}

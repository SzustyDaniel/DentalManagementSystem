using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueDisplayTvClient.Models
{
    public class Station : BindableBase
    {
        private int? _patientNumber;
        public int? PatientNumber
        {
            get { return _patientNumber; }
            set { SetProperty(ref _patientNumber, value); }
        }

        private int _stationNumber;
        public int StationNumber
        {
            get { return _stationNumber; }
            set { SetProperty(ref _stationNumber, value); }
        }
    }
}

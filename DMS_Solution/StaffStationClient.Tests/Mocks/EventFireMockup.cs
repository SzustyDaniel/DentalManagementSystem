using Prism.Events;
using StaffStationClient.Models;
using StaffStationClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Tests.Mocks
{
    public class EventFireMockup
    {
        private IEventAggregator eventAggregator;

        public StationModel StationModel { get; set; }

        public EventFireMockup(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            StationModel = new StationModel()
            {
                StationNumber = 1,
                StationServiceType = Common.ServiceType.Nurse,
                EmployeeFirstName = "Daniel",
                EmployeeLastName = "Szuster",
                EmployeeId = 1,
                Password = "1234",
                UserName = "daniel_s"
            };

        }

        public void SendModel()
        {
            eventAggregator.GetEvent<SendModelEvent>().Publish(StationModel);
        }

    }
}

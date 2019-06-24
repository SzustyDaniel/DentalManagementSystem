using Prism.Events;
using QueueRegisteringClient.Models;
using QueueRegisteringClient.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueRegisteringClient.Tests.Mocks
{
    public class EventFireMockup
    {
        IEventAggregator eventAggregator;

        public Patient Model { get; set; }

        public EventFireMockup(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;
            Model = new Patient();
        }

        public void SendModel()
        {
            eventAggregator.GetEvent<SendPatientEvent>().Publish(Model);
        }

    }
}

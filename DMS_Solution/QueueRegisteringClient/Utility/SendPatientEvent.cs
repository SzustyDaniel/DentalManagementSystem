using Prism.Events;
using QueueRegisteringClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueRegisteringClient.Utility
{
    public class SendPatientEvent: PubSubEvent<Patient>
    {
    }
}

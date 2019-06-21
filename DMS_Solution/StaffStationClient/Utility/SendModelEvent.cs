using Prism.Events;
using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Utility
{
    class SendModelEvent: PubSubEvent<StationModel>
    {
    }
}

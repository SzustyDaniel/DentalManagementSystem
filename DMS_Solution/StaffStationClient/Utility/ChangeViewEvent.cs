using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace StaffStationClient.Utility
{
    public class ChangeViewEvent: PubSubEvent<ViewType>
    {
    }
}

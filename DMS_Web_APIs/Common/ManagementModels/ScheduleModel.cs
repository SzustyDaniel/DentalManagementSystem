using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ManagementModels
{
    public class ScheduleModel
    {
        public ServiceType Type { get; set; }
        public Dictionary<DayOfWeek, WorkingWindow> Schedule { get; set; }
    }
}

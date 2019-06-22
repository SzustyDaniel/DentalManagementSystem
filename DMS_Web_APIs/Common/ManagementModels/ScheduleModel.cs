using System;
using System.Collections.Generic;
using System.Text;

namespace Common.ManagementModels
{
    public class ScheduleModel
    {
        public ServiceType Type { get; set; }
        public DayOfWeek Day { get; set; }
        public WorkingWindow WorkingHours { get; set; }
    }
}

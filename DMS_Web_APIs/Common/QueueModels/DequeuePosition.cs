using System;
using System.Collections.Generic;
using System.Text;

namespace Common.QueueModels
{
    public class DequeuePosition
    {
        public ServiceType ServiceType { get; set; }    
        public int StationNumber { get; set; }
    }
}

using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Models
{
    public class DequeueModel: BindableBase
    {
        private int queueNumber;
        public int QueueuNumber
        {
            get { return queueNumber; }
            set { SetProperty(ref queueNumber, value); }
        }

        private int customerId;
        public int CustomerId
        {
            get { return customerId; }
            set { SetProperty(ref customerId, value); }
        }

    }
}

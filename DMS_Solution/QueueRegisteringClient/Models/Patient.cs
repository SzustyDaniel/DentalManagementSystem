using Common;
using Common.QueueModels;
using Common.UserModels;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueueRegisteringClient.Models
{
    public class Patient: BindableBase
    {
        private CardInfo clientCard;
        public CardInfo ClientCard
        {
            get { return clientCard; }
            set { SetProperty(ref clientCard, value); }
        }

        private int customerID;
        public int CustomerID
        {
            get { return customerID; }
            set { SetProperty(ref customerID, value); }
        }

        private ServiceType queueType;
        public ServiceType QueueType
        {
            get { return queueType; }
            set { SetProperty(ref queueType, value); }
        }

        private EnqueuePositionResult lineNumber;
        public EnqueuePositionResult LineNumber
        {
            get { return lineNumber; }
            set { SetProperty(ref lineNumber, value); }
        }
    }
}

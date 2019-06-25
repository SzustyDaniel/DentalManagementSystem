using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Tests.Mocks
{
    public class ApplicationServiceMockup
    {
        private ApplicationServiceMockup() { }

        private static readonly ApplicationServiceMockup _instance = new ApplicationServiceMockup();

        internal static ApplicationServiceMockup Instance { get { return _instance; } }

        private IEventAggregator _eventAggregator;
        internal IEventAggregator EventAggregator
        {
            get
            {
                if (_eventAggregator == null)
                    _eventAggregator = new EventAggregator();

                return _eventAggregator;
            }
        }
    }
}

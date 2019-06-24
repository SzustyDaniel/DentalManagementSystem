using System.Collections.Generic;

namespace StaffStationClient.Tests.Mocks
{
    public class CustomerMock
    {
        public int CustomerId { get; set; }
        public ulong CardNumber { get; set; }

        public ICollection<TreatmentMock> Treatments { get; set; }
    }
}
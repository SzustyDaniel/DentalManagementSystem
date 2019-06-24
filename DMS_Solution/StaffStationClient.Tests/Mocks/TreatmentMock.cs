using System;

namespace StaffStationClient.Tests.Mocks
{
    public class TreatmentMock
    {
        public DateTime TreatmentDate { get; set; }

        public int EmployeeId { get; set; }
        public EmployeeMock Employee { get; set; }

        public int CustomerId { get; set; }
        public CustomerMock Customer { get; set; }
    }
}
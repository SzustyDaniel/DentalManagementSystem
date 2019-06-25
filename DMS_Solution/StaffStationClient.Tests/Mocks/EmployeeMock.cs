using Common;
using System.Collections.Generic;

namespace StaffStationClient.Tests.Mocks
{
    public class EmployeeMock
    {
        public int EmployeeId { get; set; }
        public ServiceType Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public bool Online { get; set; }
        public int StationId { get; set; }

        public ICollection<TreatmentMock> Treatments { get; set; }
    }
}
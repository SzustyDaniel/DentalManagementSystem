using System;

namespace UsersService.Data.Models
{
    public class Treatment
    {
        public DateTime TreatmentDate { get; set; }

        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}

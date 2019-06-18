using System.Collections.Generic;

namespace UsersService.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public ulong CardNumber { get; set; }

        public ICollection<Treatment> Treatments { get; set; }
    }
}

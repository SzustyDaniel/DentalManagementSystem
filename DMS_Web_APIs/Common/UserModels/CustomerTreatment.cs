using System;

namespace Common.UserModels
{
    public struct CustomerTreatment
    {
        public int CustomerId { get; set; }
        public int TreatingEmployeeId { get; set; }
        public DateTime DateOfTreatment { get; set; }
    }
}

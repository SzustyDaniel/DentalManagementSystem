using System;

namespace Common.UserModels
{
    public struct DailyEmployeeReport
    {
        public DateTime Date { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int NumberOfPatientsTreated { get; set; }
    }
}

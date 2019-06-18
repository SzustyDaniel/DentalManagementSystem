namespace Common.UserModels
{
    public struct EmployeeConnectionUpdate
    {
        public ServiceType ServiceType { get; set; }
        public int StationNumber { get; set; }
        public LoginStatus LoginStatus { get; set; }
    }
}

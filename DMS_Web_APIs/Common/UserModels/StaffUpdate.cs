namespace Common.UserModels
{
    public struct StaffUpdate
    {
        public ServiceType ServiceType { get; set; }
        public int StationNumber { get; set; }
        public LoginStatus LoginStatus { get; set; }
    }
}

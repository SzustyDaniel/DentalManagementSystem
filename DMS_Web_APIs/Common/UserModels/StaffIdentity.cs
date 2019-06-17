namespace Common.UserModels
{
    public struct StaffIdentity
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public ServiceType ServiceType { get; set; }
        public int StationNumber { get; set; }
    }
}

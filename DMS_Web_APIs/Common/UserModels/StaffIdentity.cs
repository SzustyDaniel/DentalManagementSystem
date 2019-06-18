namespace Common.UserModels
{
    public struct StaffIdentity
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ServiceType ServiceType { get; set; }
        public int StationNumber { get; set; }
    }
}

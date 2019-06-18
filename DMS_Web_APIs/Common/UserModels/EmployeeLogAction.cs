namespace Common.UserModels
{
    public struct EmployeeLogAction
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ServiceType ServiceType { get; set; }
        public int StationNumber { get; set; }
        public LoginStatus LoginStatus { get; set; }
    }
}

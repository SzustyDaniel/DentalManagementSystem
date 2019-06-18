using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using StaffClientApplication.Models;
using Common.UserModels;

namespace StaffClientApplication.Services
{
    public class LoginService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> SendCredentials(StationModel station)
        {
            StaffIdentity status = new StaffIdentity();
            status.UserName = station.UserName;
            status.Password = station.Password;
            status.StationNumber = station.StationNumber;
            status.ServiceType = station.StationServiceType;

            throw new NotImplementedException();
        }
    }
}

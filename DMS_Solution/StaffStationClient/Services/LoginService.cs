using StaffStationClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace StaffStationClient.Services
{
    public class LoginService
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<bool> SendCredentials(StationModel station)
        {
            // TODO create the send credentials service
            throw new NotImplementedException();
        }
    }
}

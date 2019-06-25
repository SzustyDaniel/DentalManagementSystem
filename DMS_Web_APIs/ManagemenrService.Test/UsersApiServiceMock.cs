using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.UserModels;
using ManagementService.Services;

namespace Tests
{
    internal class UsersApiServiceMock : UsersApiService
    {
        public override Task<List<DailyEmployeeReport>> GetUsersTtreatments(DateTime date)
        {
            throw new NotImplementedException();

            List<DailyEmployeeReport> reports = new List<DailyEmployeeReport>()
            {
                new DailyEmployeeReport() { Date = DateTime.Parse("2019-06-24"), FirstName = "Daniel", LastName = "Szuster"}
            };
        }
    }
}
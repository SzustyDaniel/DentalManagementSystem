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

            List<DailyEmployeeReport> reports = null;

            if(date.Date == DateTime.Parse("2019-06-24"))
            {
               reports  = new List<DailyEmployeeReport>()
            {
                new DailyEmployeeReport() { Date = DateTime.Parse("2019-06-24"), FirstName = "Daniel", LastName = "Szuster", NumberOfPatientsTreated = 3}
            };
            }

            return Task.FromResult(reports);

        }
    }
}
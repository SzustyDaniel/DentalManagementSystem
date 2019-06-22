using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.UserModels;
using Common.QueueModels;
using Common.ManagementModels;
using Common;

namespace QueueRegisteringClient.Tests.Mocks
{
    public class MockRepository
    {
        private Dictionary<CardInfo, CustomerIdentification> customerMockup;
        private List<ScheduleModel> schedules;
        private EnqueuePositionResult positionResult;

        public MockRepository()
        {
            customerMockup = new Dictionary<CardInfo, CustomerIdentification>()
            {
                { new CardInfo() { CardNumber = 200}, new CustomerIdentification() { CustomerId = 1} },
                { new CardInfo() { CardNumber = 300}, new CustomerIdentification() { CustomerId = 2} },
                { new CardInfo() { CardNumber = 400}, new CustomerIdentification() { CustomerId = 3} },
                { new CardInfo() { CardNumber = 500}, new CustomerIdentification() { CustomerId = 4} },
                { new CardInfo() { CardNumber = 600}, new CustomerIdentification() { CustomerId = 5} },
                { new CardInfo() { CardNumber = 700}, new CustomerIdentification() { CustomerId = 6} },
                { new CardInfo() { CardNumber = 800}, new CustomerIdentification() { CustomerId = 7} },
                { new CardInfo() { CardNumber = 900}, new CustomerIdentification() { CustomerId = 8} },
                { new CardInfo() { CardNumber = 1000}, new CustomerIdentification() { CustomerId = 9} }
            };

            schedules = new List<ScheduleModel>()
            {
                new ScheduleModel(){ Day = DayOfWeek.Sunday, Type = ServiceType.Nurse, WorkingHours = new WorkingWindow() { StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(10) } },
                new ScheduleModel(){ Day = DayOfWeek.Sunday, Type = ServiceType.Pharmacist, WorkingHours = new WorkingWindow() { StartTime = TimeSpan.FromHours(8), EndTime = TimeSpan.FromHours(18) } }
            };

            positionResult = new EnqueuePositionResult() { UserNumber = 1 };
        }

        internal async Task<List<ScheduleModel>> GetSchedules(DayOfWeek day)
        {
            return await Task.FromResult(schedules);
        }

        internal async Task<CustomerIdentification> GetClientId(CardInfo cardInfo)
        {
            return await Task.FromResult(customerMockup[cardInfo]);
        }

        internal async Task<EnqueuePositionResult> GetEnqueuePositionResult(EnqueuePosition requestPosition)
        {
            var task = await Task.FromResult(new EnqueuePositionResult() { UserNumber = positionResult.UserNumber });
            positionResult.UserNumber++;

            return task;
        }
    }
}
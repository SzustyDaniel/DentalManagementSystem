using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Common.QueueModels;
using Common.UserModels;

namespace StaffStationClient.Tests.Mocks
{
    public class MockRepository
    {
        private List<TreatmentMock> treatmentMocks;
        private List<EmployeeMock> employeesMocks;
        private List<CustomerMock> customerMocks;
        private LinkedList<DequeuePositionResult> nurseQueueMock;
        private LinkedList<DequeuePositionResult> pharmacyQueueMock;

        private static MockRepository _instance;
        public static MockRepository Instance { get { if (_instance == null) _instance = new MockRepository(); return _instance; } }

        private MockRepository()
        {
            employeesMocks = new List<EmployeeMock>()
            {
                new EmployeeMock(){ Firstname = "Daniel" , Lastname = "Szuster", Online = false, Password = "1234", EmployeeId = 1, Username = "daniel_s"},
                new EmployeeMock(){ Firstname = "David" , Lastname = "Fineboym", Online = false, Password = "1234", EmployeeId = 2, Username = "david_f"},
                new EmployeeMock(){ Firstname = "Shay" , Lastname = "Musachanov", Online = false, Password = "1234", EmployeeId = 3, Username = "shay_m"}
            };

            DateTime yesterday = DateTime.Today.AddDays(-1);

            treatmentMocks = new List<TreatmentMock>();

            customerMocks = new List<CustomerMock>
            {
                    new CustomerMock { CustomerId = 1, CardNumber = 100 },
                    new CustomerMock { CustomerId = 2, CardNumber = 200 },
                    new CustomerMock { CustomerId = 3, CardNumber = 300 },
                    new CustomerMock { CustomerId = 4, CardNumber = 400 },
                    new CustomerMock { CustomerId = 5, CardNumber = 500 },
                    new CustomerMock { CustomerId = 6, CardNumber = 600 },
                    new CustomerMock { CustomerId = 7, CardNumber = 700 },
                    new CustomerMock { CustomerId = 8, CardNumber = 800 },
                    new CustomerMock { CustomerId = 9, CardNumber = 900 },
                    new CustomerMock { CustomerId = 10, CardNumber = 1000 }
            };

            nurseQueueMock = new LinkedList<DequeuePositionResult>();
            nurseQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 1, CustomerNumberInQueue = 1 });
            nurseQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 2, CustomerNumberInQueue = 2 });
            nurseQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 3, CustomerNumberInQueue = 3 });
            nurseQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 4, CustomerNumberInQueue = 4 });

            pharmacyQueueMock = new LinkedList<DequeuePositionResult>();
            pharmacyQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 5, CustomerNumberInQueue = 1 });
            pharmacyQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 6, CustomerNumberInQueue = 2 });
            pharmacyQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 7, CustomerNumberInQueue = 3 });
            pharmacyQueueMock.AddLast(new DequeuePositionResult() { CustomerID = 8, CustomerNumberInQueue = 4 });

        }

        internal Task<EmployeeInfo> Login(EmployeeLogin logAction)
        {
            EmployeeMock employee = employeesMocks.Find(l => l.Username == logAction.Username && l.Password == logAction.Password);

            if (employee == null)
                throw new Exception();

            EmployeeInfo info = new EmployeeInfo()
            {
                EmployeeId = employee.EmployeeId,
                Firstname = employee.Firstname,
                Lastname = employee.Lastname
            };

            return Task.FromResult(info);
        }

        public Task<DequeuePositionResult> GetDequeuePositionNurse(DequeuePosition position)
        {
            var top = nurseQueueMock.First;
            nurseQueueMock.Remove(top);

            DequeuePositionResult result = new DequeuePositionResult() { CustomerID = top.Value.CustomerID, CustomerNumberInQueue = top.Value.CustomerNumberInQueue };

            return Task.FromResult(result);
        }

        public Task<DequeuePositionResult> GetDequeuePositionPharmacy(DequeuePosition position)
        {
            var top = pharmacyQueueMock.First;
            pharmacyQueueMock.Remove(top);

            DequeuePositionResult result = new DequeuePositionResult() { CustomerID = top.Value.CustomerID, CustomerNumberInQueue = top.Value.CustomerNumberInQueue };

            return Task.FromResult(result);
        }

        public Task ReportTretment(CustomerTreatment treatment)
        {
            TreatmentMock mock = new TreatmentMock()
            {
                Customer = new CustomerMock() { CustomerId = treatment.CustomerId },
                EmployeeId = treatment.TreatingEmployeeId,
                TreatmentDate = DateTime.Now
            };

            if (treatmentMocks.Contains(mock)) throw new Exception();

            treatmentMocks.Add(mock);

            return Task.FromResult(mock);
        } 

    }
}

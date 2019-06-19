using Common;
using Microsoft.EntityFrameworkCore;
using System;
using UsersService.Data.Models;

namespace UsersService.Data
{
    public class DataGenerator
    {
        public static void Initialize(DbContextOptions<UsersContext> contextOptions)
        {
            using (var context = new UsersContext(contextOptions))
            {
                DateTime yesterday = DateTime.Today.AddDays(-1);

                context.Customers.AddRange(
                    new Customer { CustomerId = 1, CardNumber = 100 },
                    new Customer { CustomerId = 2, CardNumber = 200 },
                    new Customer { CustomerId = 3, CardNumber = 300 },
                    new Customer { CustomerId = 4, CardNumber = 400 },
                    new Customer { CustomerId = 5, CardNumber = 500 },
                    new Customer { CustomerId = 6, CardNumber = 600 },
                    new Customer { CustomerId = 7, CardNumber = 700 },
                    new Customer { CustomerId = 8, CardNumber = 800 },
                    new Customer { CustomerId = 9, CardNumber = 900 },
                    new Customer { CustomerId = 10, CardNumber = 1000 }
                    );

                context.Employees.AddRange(
                    new Employee
                    {
                        EmployeeId = 1,
                        Role = ServiceType.Nurse,
                        Username = "shay_m",
                        Password = "qwerty",
                        Firstname = "Shay",
                        Lastname = "Musachanov",
                        Email = "chookity@gmail.com"
                    },
                    new Employee
                    {
                        EmployeeId = 2,
                        Role = ServiceType.Pharmacist,
                        Username = "daniel_s",
                        Password = "1234",
                        Firstname = "Daniel",
                        Lastname = "Szuster",
                        Email = "archer@gmail.com"
                    },
                    new Employee
                    {
                        EmployeeId = 3,
                        Role = ServiceType.Pharmacist,
                        Username = "david_f",
                        Password = "unabomber",
                        Firstname = "David",
                        Lastname = "Fineboym",
                        Email = "rickandmorty@gmail.com"
                    }
                    );

                context.Treatments.AddRange(
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 1, CustomerId = 1 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 1, CustomerId = 2 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 2, CustomerId = 3 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 2, CustomerId = 4 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 2, CustomerId = 5 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 3, CustomerId = 6 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 3, CustomerId = 7 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 1, CustomerId = 7 },
                    new Treatment { TreatmentDate = yesterday, EmployeeId = 1, CustomerId = 6 }
                    );

                context.SaveChanges();
            }
        }
    }
}

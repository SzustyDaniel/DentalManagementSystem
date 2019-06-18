using Common;
using Microsoft.EntityFrameworkCore;
using UsersService.Data.Models;

namespace UsersService.Data
{
    public class DataGenerator
    {
        public static void Initialize(DbContextOptions<UsersContext> contextOptions)
        {
            using (var context = new UsersContext(contextOptions))
            {
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

                context.SaveChanges();
            }
        }
    }
}

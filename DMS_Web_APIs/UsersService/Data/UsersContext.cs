using Microsoft.EntityFrameworkCore;
using UsersService.Data.Models;

namespace UsersService.Data
{
    public class UsersContext : DbContext
    {
        public UsersContext(DbContextOptions<UsersContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Treatment> Treatments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Treatment>().HasKey(t => new { t.TreatmentDate, t.CustomerId, t.EmployeeId });
        }
    }
}

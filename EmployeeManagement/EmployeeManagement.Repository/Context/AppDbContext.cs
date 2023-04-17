using EmployeeManagement.Contracts.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.Repository.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }


        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    }
}

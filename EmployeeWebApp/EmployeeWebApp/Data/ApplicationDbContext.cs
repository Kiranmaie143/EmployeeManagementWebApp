using Microsoft.EntityFrameworkCore;
using EmployeeWebApp.Models;

namespace EmployeeWebApp.Data
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>option):base(option)
        {
        }
        public DbSet<Employee> Employees { get; set; }

    }
}

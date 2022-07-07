
using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.DatabaseContexts
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {

        }
        public DbSet<
            Employee> Employees
        { get; set; }
    }
}

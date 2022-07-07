using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Models
{
    public class EmployeeContext : DbContext
    {

        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }
        public DbSet<
            Employee> Employees
        { get; set; }

    }
}

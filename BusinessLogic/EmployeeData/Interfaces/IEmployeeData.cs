
using BusinessLogic.Models;

namespace BusinessLogic.EmployeeData.Interfaces
{
    public interface IEmployeeData
    {
        public List<Employee> GetEmployees();

        public Employee GetEmployee(Guid id);

        public Employee CreateEmployee(Employee employee);

        public Employee UpdateEmployee(Employee employee);

        public void DeleteEmployee(Employee employee);
    }
}

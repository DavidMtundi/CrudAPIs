using FinalCRUD.Models;

namespace FinalCRUD.EmployeeData
{
    public interface IEmployeeData
    {
        List<Employee> GetEmployees();

        Employee GetEmployee(Guid id);

        Employee CreateEmployee(Employee employee);

        Employee UpdateEmployee(Employee employee);

        void DeleteEmployee(Employee employee);
    }
}

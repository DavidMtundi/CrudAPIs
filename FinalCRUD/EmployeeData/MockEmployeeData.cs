using FinalCRUD.Models;

namespace FinalCRUD.EmployeeData
{
    public class MockEmployeeData : IEmployeeData
    {
        private List<Employee> _employees = new List<Employee>()
        {
            new Employee(){ Id = Guid.NewGuid(), Name = "Employee one"},
            new Employee(){ Id = Guid.NewGuid(), Name = "Employee Two"},
            new Employee(){ Id = Guid.NewGuid(), Name = "Employee Three"},
            new Employee(){ Id = Guid.NewGuid(), Name = "Employee Four"},
        };

        public Employee CreateEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _employees.Add(employee);
            return employee;
        }

        public void DeleteEmployee(Employee employee)
        {
            _employees.Remove(employee);
        }

        public Employee GetEmployee(Guid id) => _employees.SingleOrDefault(x => x.Id == id)!;

        public List<Employee> GetEmployees()
        {
            return _employees;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var existingEmployee = GetEmployee(employee.Id);
            existingEmployee.Name = employee.Name;
            return existingEmployee;
        }
    }
}

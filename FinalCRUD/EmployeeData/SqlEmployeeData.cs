using FinalCRUD.Models;

namespace FinalCRUD.EmployeeData
{
    public class SqlEmployeeData : IEmployeeData
    {
        private EmployeeContext _employeeContext;
        public SqlEmployeeData(EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
        }

        public Employee CreateEmployee(Employee employee)
        {
            employee.Id = Guid.NewGuid();
            _employeeContext.Employees.Add(employee);
            _employeeContext.SaveChanges();
            return employee;
        }



        public void DeleteEmployee(Employee employee)
        {

            _employeeContext.Employees.Remove(employee);
            _employeeContext.SaveChanges();

        }



        public Employee GetEmployee(Guid id)
        {
            var employee = _employeeContext.Employees.Find(id);
            return employee!;
        }

        public List<Employee> GetEmployees()
        {
            return _employeeContext.Employees.ToList();
        }

        public Employee UpdateEmployee(Employee employee)
        {
            var existingEmployee = _employeeContext.Employees.Find(employee.Id);

            if (existingEmployee != null)
            {

                existingEmployee.Name = employee.Name;
                _employeeContext.Employees.Update(existingEmployee);
                _employeeContext.SaveChanges();
            }
            return employee;
        }


    }
}

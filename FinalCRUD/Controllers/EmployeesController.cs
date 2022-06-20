using FinalCRUD.EmployeeData;
using FinalCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace FinalCRUD.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;

        public EmployeesController(IEmployeeData employeeData)
        {
            _employeeData = employeeData;
        }


        [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]

        public IActionResult GetEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee != null)
            {
                return Ok(employee);
            }
            return NotFound($"Employee with id: {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]

        public IActionResult GetEmployee(Employee employee)
        {
            _employeeData.CreateEmployee(employee);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]

        public IActionResult DeleteEmployee(Guid id)
        {
            var employee = _employeeData.GetEmployee(id);
            if (employee != null)
            {
                _employeeData.DeleteEmployee(employee);
                return Ok($"Employee with id: {id} was deleted successfully");
            }
            return NotFound($"Employee with id: {id} was not found");
        }

        [HttpPatch]
        [Route("api/[controller]/{id}")]

        public IActionResult UpdateEmployee(Guid id, Employee employee)
        {
            var existingEmployee = _employeeData.GetEmployee(id);
            if (existingEmployee != null)
            {
                employee.Id = existingEmployee.Id;
                _employeeData.UpdateEmployee(employee);
                return Ok($"Employee with id: {id} was updated successfully");
            }
            return NotFound($"Employee with id: {id} was not found");
        }
    }
}

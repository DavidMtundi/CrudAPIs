using FinalCRUD.AuthManagers;
using FinalCRUD.EmployeeData;
using FinalCRUD.Models;
using FinalCRUD.Userservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinalCRUD.Controllers
{
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private IEmployeeData _employeeData;
        private readonly JwtAuthManager jwtAuthManager;


        public EmployeesController(IEmployeeData employeeData, JwtAuthManager jwtAuthManager)
        {
            _employeeData = employeeData;
            this.jwtAuthManager = jwtAuthManager;
        }

        [Authorize]
        [HttpGet]
        [Route("api/[controller]")]

        public IActionResult GetEmployees()
        {
            return Ok(_employeeData.GetEmployees());
        }


        [Authorize]
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



        [Authorize]
        [HttpPost]
        [Route("api/[controller]")]

        public IActionResult GetEmployee(Employee employee)
        {
            _employeeData.CreateEmployee(employee);
            return Created(HttpContext.Request.Scheme + "://" + HttpContext.Request.Host + HttpContext.Request.Path + "/" + employee.Id, employee);
        }



        [Authorize]
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



        [Authorize]
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


        [HttpPost("authorize")]
        public IActionResult AuthUser([FromBody] User user)
        {
            var token = jwtAuthManager.authenticate(user.Username!, user.Password!);
            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }

    }
}

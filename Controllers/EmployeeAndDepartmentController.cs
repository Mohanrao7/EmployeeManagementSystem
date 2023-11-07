
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Models.Dto;
using EmployeeManagementApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EEmployeeManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeAndDepartmentController : ControllerBase
    {
        private IApiServices services;

        public EmployeeAndDepartmentController(IApiServices services)
        {
            this.services=services;
        }
        [HttpPost]
        [Route("AddEmployeeAndDepartment")]
     
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> AddEmployeeAndDepartment([FromBody] EmployeeDto employee)
        {
            var response=new ResponseDto();
            var employeeModel = new Employee
            {
                FirstName = employee.FirstName,
                LastName = employee.LastName,
                Status = employee.Status,
                ContactNumber = employee.ContactNumber
            };
            
            var employeeResponse=await services.AddEmployee(employeeModel, response);
            
            var departmentDto = employee.Department;
            var departmentModel = new DepartmentDto
            {
                Name = departmentDto.Name
            };

            if(await services.CheckDepartmentExisted(departmentModel.Name, response))
            {
                await services.AddEmployeeDepartmentAssociation(response);
            }
            else
            {
                var departmentResponse = await services.AddDepartment(departmentModel, response);


                await services.AddEmployeeDepartmentAssociation(response);
            }


            return Ok(new { Employee = employeeResponse, /*Department = departmentResponse*/ });
        }
        [HttpDelete]
        [Route("DeleteEmployees")]
        public async Task<IActionResult> DeleteEmployee([FromQuery] Guid Id) 
        {
            var response = await services.DeleteEmployee(Id);
            if (response == null)
            {
                return NotFound();
            }

            return Ok(response);
        }
        [HttpDelete]
        [Route("DeleteDepartment")]
        public async Task<IActionResult> DeleteDepartment([FromQuery] Guid Id)
        {
            var response = await services.DeleteDepartment(Id);
            if (response == null)
            {
                return NotFound();
            }
            return Ok(response);


        }

        


    }
}


using EmployeeManagementApi.Models;
using EmployeeManagementApi.Models.Dto;

namespace EmployeeManagementApi.Services
{
    public interface IApiServices
    {
        Task<Employee> AddEmployee(Employee employee, ResponseDto response);
        Task<Employee> DeleteEmployee(Guid id);

        Task<Department> AddDepartment(DepartmentDto department, ResponseDto response);

        Task<Department> DeleteDepartment(Guid id);

        Task<EmployeeDepartmentAssociation> AddEmployeeDepartmentAssociation(ResponseDto response);
        Task<bool> CheckDepartmentExisted(string Name, ResponseDto response);
    }
}

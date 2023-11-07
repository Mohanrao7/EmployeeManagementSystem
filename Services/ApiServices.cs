
using EmployeeManagementApi.Data;
using EmployeeManagementApi.Models;
using EmployeeManagementApi.Models.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EmployeeManagementApi.Services { 
    public class ApiServices : IApiServices
    {
        private EmployeeDbContext db;

        public ApiServices(EmployeeDbContext db)
        {
            this.db = db;
        }

        public async Task<Department> AddDepartment(DepartmentDto department, ResponseDto response)
        {
            var deaprtmentModel = new Department
            {
                Name = department.Name,
            };
            await db.Departments.AddAsync(deaprtmentModel);
            await db.SaveChangesAsync();
            response.DepartmentId = deaprtmentModel.Id;
            response.DepartmentName = deaprtmentModel.Name;
            

            return (deaprtmentModel);
        }

        public async Task<Employee> AddEmployee(Employee employee, ResponseDto response)
        {
            await db.Employees.AddAsync(employee);
            await db.SaveChangesAsync();

            response.EmployeeId= employee.Id;
            
            return (employee);

        }

        public async Task<EmployeeDepartmentAssociation> AddEmployeeDepartmentAssociation(ResponseDto response)
        {

           
            var existingDepartment = await db.Departments
                .Where(d => d.Name == response.DepartmentName)
                .FirstOrDefaultAsync();

            if (existingDepartment != null)
            {
                // Check if the association already exists
                var existingAssociation = await db.employeeDepartmentAssociations
                    .Where(e => e.EmployeeId == response.EmployeeId && e.DepartmentId == existingDepartment.Id)
                    .FirstOrDefaultAsync();

                if (existingAssociation != null)
                {
                    return existingAssociation;
                }

                // If it doesn't exist, create and add the new association
                var association = new EmployeeDepartmentAssociation
                {
                    EmployeeId = response.EmployeeId,
                    DepartmentId = existingDepartment.Id
                };

                await db.employeeDepartmentAssociations.AddAsync(association);
                await db.SaveChangesAsync();

                return association;
            }

            
            return null;
        }

        public async Task<Department> DeleteDepartment(Guid id)
        {
            var existingDepartment = await db.Departments.FirstOrDefaultAsync(x => x.Id == id);
            if (existingDepartment == null)
            {
                return null;
            }
            db.Departments.Remove(existingDepartment);
            await db.SaveChangesAsync();
            return existingDepartment;
        }

        public async Task<Employee> DeleteEmployee(Guid id)
        {
            var existing = await db.Employees.FirstOrDefaultAsync(x => x.Id == id);
            if (existing == null)
            {
                return null;
            }
            db.Employees.Remove(existing);
            await db.SaveChangesAsync();
            return existing;
        }
        public async Task<bool> CheckDepartmentExisted(string Name, ResponseDto response)
        {
            var existed= await db.Departments.FirstOrDefaultAsync(x =>x.Name == Name);
            if (existed != null)
            {
                response.DepartmentId = existed.Id;
                response.DepartmentName = existed.Name;
                return true;
            }
            
            return false;
            
            
        }
    }
}

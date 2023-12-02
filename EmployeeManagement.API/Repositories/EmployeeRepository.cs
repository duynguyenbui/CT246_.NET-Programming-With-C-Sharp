using EmployeeManagement.API.Data;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Repositories.Contracts;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManagement.API.Repositories;

public class EmployeeRepository(EmpManagementContext context) : IEmployeeRepository
{
    public async Task<IEnumerable<Employee>> GetEmployees()
    {
        return await context.Employees
            .Include(employee => employee.Department)
            .ToListAsync();
    }

    public async Task<Employee?> GetEmployee(int employeeId)
    {
        return await context.Employees.Include(employee => employee.Department)
            .FirstOrDefaultAsync(employee => employee.EmployeeId == employeeId);
    }

    public async Task<Employee> AddEmployee(Employee employee)
    {
        var entry = await context.Employees.AddAsync(employee);
        await context.SaveChangesAsync();
        return entry.Entity;
    }

    public async Task<Employee?> UpdateEmployee(Employee employee)
    {
        var result = await context.Employees
            .FirstOrDefaultAsync(e => e.EmployeeId == employee.EmployeeId);
        if (result is not null)
        {
            result.FirstName = employee.FirstName;
            result.LastName = employee.LastName;
            result.Email = employee.Email;
            result.DateOfBirth = employee.DateOfBirth;
            result.Gender = employee.Gender;
            result.DepartmentId = employee.DepartmentId;
            result.PhotoPath = employee.PhotoPath;
        }
        var isSucceeded = await context.SaveChangesAsync();
        
        return isSucceeded > 0 ? result : null;
    }

    public async Task<Employee?> DeleteEmployee(int employeeId)
    {
        var employee = await context.Employees.FindAsync(employeeId);
        if (employee != null)
        {
            var entry = context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return entry.Entity;
        }

        return null;
    }
}
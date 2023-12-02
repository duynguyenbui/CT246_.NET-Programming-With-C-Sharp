using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Entities;
using EmployeeManagement.API.Repositories.Contracts;

namespace EmployeeManagement.API.Repositories;

public class DepartmentRepository(EmpManagementContext context) : IDepartmentRepository
{
    public async Task<Department> CreateDepartment(Department department)
    {
        var entry = await context.Deparments.AddAsync(department);
        var result = await context.SaveChangesAsync();
        if (result > 0)
        {
            return entry.Entity;
        }

        return null;
    }
    
    public async Task<int> DeleteDepartment(int id)
    {
        var department = await context.Deparments.FindAsync(id);
        if (department is not null)
        {
            context.Deparments.Remove(department);
            var result = await context.SaveChangesAsync();
            return result;
        }

        return 0;
    }
}
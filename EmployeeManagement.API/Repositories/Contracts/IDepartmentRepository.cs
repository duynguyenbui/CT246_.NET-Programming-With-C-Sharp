using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Entities;

namespace EmployeeManagement.API.Repositories.Contracts;

public interface IDepartmentRepository
{
    public Task<Department> CreateDepartment(Department department);
    public Task<int> DeleteDepartment(int id);
}
using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Web.Services.Contracts;

public interface IDepartmentService
{
    Task<IEnumerable<Department>> GetDepartments();
    Task<HttpResponseMessage> DeleteDepartment(int id);
    Task<Department> CreateDepartment(CreateDepartmentDto createDepartmentDto);

}
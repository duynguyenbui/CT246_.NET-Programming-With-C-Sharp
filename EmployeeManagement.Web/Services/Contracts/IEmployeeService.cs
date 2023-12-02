using EmployeeManagement.Web.Models;

namespace EmployeeManagement.Web.Services.Contracts;

public interface IEmployeeService
{
    Task<IEnumerable<Employee>?> GetEmployees();

    Task<Employee> GetEmployee(int id);
    
    Task<HttpResponseMessage> AddEmployee(CreateEmployeeDto createEmployeeDto);
    
    Task<HttpResponseMessage> DeleteEmployee(int id);
    Task<HttpResponseMessage> UpdateEmployee(UpdateEmployeeDto employee);
    Task<Employee> GetEmployeeById(int id);
}
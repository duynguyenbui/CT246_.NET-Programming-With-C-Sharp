using EmployeeManagement.API.Entities;

namespace EmployeeManagement.API.Repositories.Contracts;

public interface IEmployeeRepository
{
    Task<IEnumerable<Employee>> GetEmployees();
    Task<Employee?> GetEmployee(int employeeId);
    Task<Employee> AddEmployee(Employee employee);
    Task<Employee?> UpdateEmployee(Employee employee);
    Task<Employee> DeleteEmployee(int employeeId);
}
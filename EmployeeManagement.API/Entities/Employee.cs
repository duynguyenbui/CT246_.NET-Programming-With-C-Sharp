using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Entities;

public class Employee
{
    public int EmployeeId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    [EmailAddress]
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    [Range(0,2)]
    public Gender Gender { get; set; }
    public int DepartmentId { get; set; }
    public Department Department { get; set; }
    public string PhotoPath { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.API.Entities;

public class Department
{
    public int DepartmentId { get; set; }
    [Required]
    public string DepartmentName { get; set; }
}
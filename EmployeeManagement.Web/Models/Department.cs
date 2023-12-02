using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models;

public class Department
{
    public int DepartmentId { get; set; }
    [Required]
    public string DepartmentName { get; set; }
}
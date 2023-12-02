using Microsoft.AspNetCore.Components.Forms;

namespace EmployeeManagement.Web.Models;

public class CreateEmployeeDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public int Gender { get; set; }
    public int DepartmentId { get; set; }
    public IBrowserFile File { get; set; }
}
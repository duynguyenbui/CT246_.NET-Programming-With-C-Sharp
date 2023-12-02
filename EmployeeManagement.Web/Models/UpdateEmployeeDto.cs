using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Models;

public class UpdateEmployeeDto
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
    [FileExtensions(Extensions = ".jpg,.jpeg,.png,.gif",
        ErrorMessage = "Please upload a valid image file.")]
    public string PhotoPath { get; set; }
}
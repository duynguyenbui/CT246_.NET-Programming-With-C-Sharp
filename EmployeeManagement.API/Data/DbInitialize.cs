using EmployeeManagement.API.Entities;
using Microsoft.AspNetCore.Identity;

namespace EmployeeManagement.API.Data;

public static class DbInitialize
{
    public static async Task Initialize(EmpManagementContext context, UserManager<IdentityUser> userManager)
    {
        if (!userManager.Users.Any())
        {
            var admin = new IdentityUser()
            {
                UserName = "admin",
                Email = "admin@test.com"
            };
            
            await userManager.CreateAsync(admin, "Pa$$w0rd");
            await userManager.AddToRolesAsync(admin, new[] {"Admin"});
            
            var user = new IdentityUser()
            {
                UserName = "user",
                Email = "user@test.com"
            };
            
            await userManager.CreateAsync(user, "Pa$$w0rd");
            await userManager.AddToRolesAsync(user, new[] {"Member"});
        }
    
        
        if(context.Deparments.Any() || context.Employees.Any()) return;

        Department[] departments = {
            new() { DepartmentId = 1, DepartmentName = "IT" },
            new() { DepartmentId = 2, DepartmentName = "HR" },
            new() { DepartmentId = 3, DepartmentName = "Payroll" },
            new() { DepartmentId = 4, DepartmentName = "Admin" },
        };

        await context.Deparments.AddRangeAsync(departments);
        
        Employee[] employees = {
            new()
            {
                EmployeeId = 1,
                FirstName = "John",
                LastName = "Hastings",
                Email = "David@pragimtech.com",
                DateOfBirth = new DateTime(1980, 10, 5),
                Gender = Gender.Male,
                DepartmentId = 1,
                PhotoPath = "images/john.png"
            },
            new()
            {
                EmployeeId = 2,
                FirstName = "Sam",
                LastName = "Galloway",
                Email = "Sam@pragimtech.com",
                DateOfBirth = new DateTime(1981, 12, 22),
                Gender = Gender.Male,
                DepartmentId = 2,
                PhotoPath = "images/sam.jpg"
            },
            new()
            {
                EmployeeId = 3,
                FirstName = "Mary",
                LastName = "Smith",
                Email = "mary@pragimtech.com",
                DateOfBirth = new DateTime(1979, 11, 11),
                Gender = Gender.Female,
                DepartmentId = 1,
                PhotoPath = "images/mary.png"
            },
            new()
            {
                EmployeeId = 4,
                FirstName = "Sara",
                LastName = "Longway",
                Email = "sara@pragimtech.com",
                DateOfBirth = new DateTime(1982, 9, 23),
                Gender = Gender.Female,
                DepartmentId = 3,
                PhotoPath = "images/sara.png"
            }
        };
        
        await context.Employees.AddRangeAsync(employees);
        await context.SaveChangesAsync();
    }
}
using System.Text.Json;
using AutoMapper;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components;

namespace EmployeeManagement.Web.Pages;

public class EditEmployeeBase : ComponentBase
{
    [Parameter]
    public int Id { get; set; }
    
    public Employee Employee { get; set; }
    public IEnumerable<Department> Departments { get; set; }

    [Inject] public IMapper Mapper { get; set; }
    [Inject] public IEmployeeService EmployeeService { get; set; }
    [Inject] public IDepartmentService DepartmentService { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Employee = await EmployeeService.GetEmployeeById(Id);
        Departments = await DepartmentService.GetDepartments();
        Console.WriteLine(Employee);
    }

    public async Task UpdateEmployee()
    {
        Console.WriteLine(Employee);
        
        var updateEmployeeDto = Mapper.Map<UpdateEmployeeDto>(Employee);
        HttpResponseMessage response = await EmployeeService.UpdateEmployee(updateEmployeeDto);

        if (response.IsSuccessStatusCode)
        {
            // Read the content of the response, assuming it's in JSON format
            string content = await response.Content.ReadAsStringAsync();

            // Deserialize the content to an Employee object
            Employee updatedEmployee = JsonSerializer.Deserialize<Employee>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (updatedEmployee != null)
            {
                // Redirect to the EmployeeDetails page after successful update
                NavigationManager.NavigateTo($"/EmployeeDetails/{updatedEmployee.EmployeeId}", forceLoad: true);
            }
        }
    }
}
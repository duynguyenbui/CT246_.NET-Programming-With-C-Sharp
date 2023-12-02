using System.Security.Claims;
using Microsoft.AspNetCore.Components;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services.Contracts;
using Microsoft.AspNetCore.Components.Authorization;

namespace EmployeeManagement.Web.Pages;

public class EmployeeDetailsBase : ComponentBase
{
    [Inject] public IEmployeeService EmployeeService { set; get; }
    
    [Parameter]
    public int Id { get; set; }
    
    public Employee Employee { set; get; }

    
    protected override async Task OnInitializedAsync()
    {
        Employee = await EmployeeService.GetEmployee(Id);
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        User = authState.User;
    }
    
    [Inject]
    public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    
    protected ClaimsPrincipal User;
    
    [Inject]
    public NavigationManager NavigationManager { get; set; }
    
    public async Task DeleteEmployee(int employeeId)
    {
        var response = await EmployeeService.DeleteEmployee(employeeId);
        if (response.IsSuccessStatusCode)
        {
            NavigationManager.NavigateTo("/EmployeeList", forceLoad: true);
        }
    }
}
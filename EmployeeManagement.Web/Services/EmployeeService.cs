using System.Net.Http.Headers;
using System.Net.Http.Json;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services.Contracts;
using Microsoft.JSInterop;

namespace EmployeeManagement.Web.Services;

public class EmployeeService(HttpClient client, IJSRuntime JSRuntime) : IEmployeeService
{
    public async Task<IEnumerable<Employee>?> GetEmployees()
    {
        await SetAuthorizationHeader(client);
        return await client.GetFromJsonAsync<Employee[]>("api/Employees");
    }

    public async Task<Employee> GetEmployee(int id)
    {
        await SetAuthorizationHeader(client);
        return await client.GetFromJsonAsync<Employee>($"api/Employees/{id}");
    }

    public async Task<HttpResponseMessage> AddEmployee(CreateEmployeeDto createEmployeeDto)
    {
        throw new NotImplementedException();
    }

    public async Task<HttpResponseMessage> DeleteEmployee(int id)
    {
        await SetAuthorizationHeader(client);
        return await client.DeleteAsync($"api/Employees/{id}");
    }

    public async Task<HttpResponseMessage> UpdateEmployee(UpdateEmployeeDto employee)
    {
        await SetAuthorizationHeader(client);
        return await client.PatchAsJsonAsync("/api/Employees", employee);
    }

    public async Task<Employee> GetEmployeeById(int id)
    {
        await SetAuthorizationHeader(client);
        return await client.GetFromJsonAsync<Employee>($"/api/Employees/{id}");
    }
    
    private async Task<string> GetToken()
    {
        var token = await JSRuntime.InvokeAsync<string>("localStorageManager.getItem", "jwtToken");
        return token;
    }
    
    private async Task SetAuthorizationHeader(HttpClient client)
    {
        string token = await GetToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }
}
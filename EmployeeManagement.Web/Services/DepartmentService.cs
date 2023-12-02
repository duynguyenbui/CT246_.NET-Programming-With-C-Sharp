using System.Net.Http.Headers;
using System.Net.Http.Json;
using EmployeeManagement.Web.Models;
using EmployeeManagement.Web.Services.Contracts;
using Microsoft.JSInterop;

namespace EmployeeManagement.Web.Services;

public class DepartmentService(HttpClient client, IJSRuntime JsRuntime) : IDepartmentService
{
    public async Task<IEnumerable<Department>> GetDepartments()
    {
        string token = await GetToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await client.GetFromJsonAsync<Department[]>("/api/departments");
    }
    
    public async Task<HttpResponseMessage> DeleteDepartment(int id)
    {
        string token = await GetToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        return await client.DeleteAsync($"/api/departments/{id}");
    }

    public async Task<Department> CreateDepartment(CreateDepartmentDto createDepartmentDto)
    {
        string token = await GetToken();
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await client.PostAsJsonAsync<CreateDepartmentDto>("api/departments", createDepartmentDto);
        return await response.Content.ReadFromJsonAsync<Department>();
    }

    private async Task<string> GetToken()
    {
        var token = await JsRuntime.InvokeAsync<string>("localStorageManager.getItem", "jwtToken");
        return token;
    }
}
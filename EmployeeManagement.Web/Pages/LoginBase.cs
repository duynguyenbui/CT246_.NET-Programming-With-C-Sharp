using System.Net.Http.Json;
using EmployeeManagement.Web.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EmployeeManagement.Web.Pages;

public class LoginBase : ComponentBase
{
    [Inject] public HttpClient Client { get; set; }
    [Inject] public NavigationManager NavigationManager { get; set; }
    [Inject] public IJSRuntime JsRuntime { get; set; }

    public LoginDto LoginDto { get; set; } = new();
    public bool loginFailed;
    // public UserDto UserDto { get; set; } = new();
    public async Task SubmitLogin()
    {
        try
        {
            if (!string.IsNullOrEmpty(LoginDto.Username) && !string.IsNullOrEmpty(LoginDto.Password))
            {
                var httpResponse = await Client.PostAsJsonAsync("api/Account/login", LoginDto);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var userDto = await httpResponse.Content.ReadFromJsonAsync<UserDto>();
                    await SaveToken(userDto.Token); 
                    NavigationManager.NavigateTo("EmployeeList", forceLoad: true);
                }
                else
                {
                    loginFailed = true;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    
    private async Task SaveToken(string token)
    {
        await JsRuntime.InvokeVoidAsync("localStorageManager.setItem", "jwtToken", token);
    }
}
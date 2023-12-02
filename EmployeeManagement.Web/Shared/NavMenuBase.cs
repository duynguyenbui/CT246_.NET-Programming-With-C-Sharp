using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace EmployeeManagement.Web.Shared;

public class NavMenuBase : ComponentBase
{
    [Inject] public IJSRuntime JsRuntime { set; get; }
    [Inject] public NavigationManager NavigationManager { set; get; }
    
    public bool collapseNavMenu = true;

    public string? NavMenuCssClass => collapseNavMenu ? "collapse" : null;

    public void ToggleNavMenu()
    {
        collapseNavMenu = !collapseNavMenu;
    }

    public async Task DeleteCookie()
    {
        await JsRuntime.InvokeVoidAsync("localStorageManager.removeItem", "jwtToken");
        NavigationManager.NavigateTo("/", forceLoad: true);
    }

}
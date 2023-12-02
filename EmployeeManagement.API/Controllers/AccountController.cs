using EmployeeManagement.API.Data;
using EmployeeManagement.API.DTOs;
using EmployeeManagement.API.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController(UserManager<IdentityUser> userManager, EmpManagementContext context,
        TokenService tokenService)
    : ControllerBase
{
    [HttpPost("login")]
    public async Task<ActionResult<UserDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await userManager.FindByNameAsync(loginDto.Username);
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDto.Password))
            return Unauthorized();
        var token = await tokenService.GenerateToken(user);
        return Ok(new UserDto
        {
            Email = user.Email,
            Token = token
        });
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(RegisterDto registerDto)
    {
        var existUser = await userManager.FindByNameAsync(registerDto.Username);
        if (existUser is not null) return Problem("User already registered");

        var user = new IdentityUser
        {
            UserName = registerDto.Username, Email = registerDto.Email
        };
        var result = await userManager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            return ValidationProblem();
        }

        await userManager.AddToRoleAsync(user, "Member");

        return StatusCode(StatusCodes.Status201Created);
    }
}
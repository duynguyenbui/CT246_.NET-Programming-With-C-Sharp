using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EmployeeManagement.API.Services;

public class TokenService(UserManager<IdentityUser> userManager, IConfiguration configuration)
{
    public async Task<string> GenerateToken(IdentityUser user)
    {
        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, user.Email!),
            new Claim(ClaimTypes.Name, user.UserName!),
        };
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:TokenKey"]!));
        var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
        var tokenOptions = new JwtSecurityToken(
            issuer: null,
            audience: null,
            claims,
            expires: DateTime.Now.AddDays(2),
            signingCredentials: cred
        );
        return new JwtSecurityTokenHandler().WriteToken(tokenOptions);
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace Vue_DotNet_JWT.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("login")]
    public IActionResult Login()
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("WPCBOXRYKSEFUTWL6QT6RXHYD424JFB3"));
        var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, "user@example.com"),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim("role", "admin"),
        };

        var token = new JwtSecurityToken(
            issuer: "your-issuer",
            audience: "your-audience",
            claims: claims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: signingCredentials
        );
        
        var tokenHandler = new JwtSecurityTokenHandler();
        var tokenString = tokenHandler.WriteToken(token);
        
        return Ok(new { token = tokenString });
    }
    
    [Authorize(Policy = "AdminOnly")]
    [HttpGet("GetRoles")]
    public IActionResult GetRoles()
    {
        var jwtToken = HttpContext.Request.Headers["Authorization"].ToString();
        if (jwtToken.StartsWith("Bearer "))
        {
            jwtToken = jwtToken.Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.ReadJwtToken(jwtToken);
            
            var claims = token.Claims;
            var roleClaims = claims.Where(c => c.Type == "role").Select(c => c.Value).ToList();
            
            return Ok(roleClaims);
        }
        
        return Unauthorized();
    }
}
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using ToDoApi.Shared.Interfaces;
using ToDoApi.Shared.Models;

namespace ToDoApi.Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _configuration;
    private readonly List<UserModel> _users = new List<UserModel>
    {
        new UserModel { UserName = "test", Password = "password" }
    };

    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string Authenticate(string username, string password)
    {
        var user = _users.SingleOrDefault(x => x.UserName == username && x.Password == password);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or password");
        }

        var confkey = _configuration["Jwt:Key"];
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:Audience"];
        if (!double.TryParse(_configuration["Jwt:DurationInMinutes"], out double durationInMinutes))
        {
            durationInMinutes = 60;
        }
        if (confkey is null)
        {
            throw new UnauthorizedAccessException("Jwt:Key is not set in configuration");
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(confkey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.UserName)
            ]),
            Expires = DateTime.UtcNow.AddMinutes(durationInMinutes),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
            Issuer = issuer,
            Audience = audience
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }
}

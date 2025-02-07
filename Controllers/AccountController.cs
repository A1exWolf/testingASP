using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using TestingBackend.Data;
using TestingBackend.Models;

namespace TestingBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(User user)
    {
        if (await _context.Users.FirstOrDefaultAsync(u => u.Email == user.Email) != null)
            return new OkObjectResult("Пользователь с таким email существует.");

        var hasher = new PasswordHasher<User>();
        var tempUser = new User { Email = user.Email, Name = user.Name, Role = user.Role };
        tempUser.Password = hasher.HashPassword(user, user.Password);

        _context.Users.AddAsync(tempUser);
        await _context.SaveChangesAsync();
        
        return new OkObjectResult("Регистрация прошла успешно.");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginAccount login)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == login.Email);
        if (user == null)
        {
            return Unauthorized("Неверный логин или пароль.");
        }

        var hasher = new PasswordHasher<User>();
        var verificationResult = hasher.VerifyHashedPassword(user, user.Password, login.Password);
        if (verificationResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized("Неверный логин или пароль.");
        }

        // Формируем сведений о пользователе, которые будут включены в JWT.
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, user.Role.ToString()),
            new Claim(ClaimTypes.Sid, user.Id.ToString())
        };

        var now = DateTime.UtcNow;

        var jwt = new JwtSecurityToken(
            issuer: "MyAuthServer", // Издатель токена
            audience: "MyAuthClient", // Потребитель токена
            claims: claims, // Полезная нагрузка с данными о пользователе
            notBefore: now, // Токен действителен с текущего момента
            expires: now.AddMinutes(60), // Токен истекает через 60 минут
            signingCredentials: new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes("wbNrGhnhSAEOp01FMczd6GxNHoyrAuW2")),
                SecurityAlgorithms.HmacSha256) // Алгоритм подписи токена (HMAC-SHA256)
        );

        // Генерируем строковое представление JWT.
        var token = new JwtSecurityTokenHandler().WriteToken(jwt);

        return Ok(new { access_token = token, email = user.Email });
    }

    
}
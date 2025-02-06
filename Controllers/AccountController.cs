using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
    
}
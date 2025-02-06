using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TestingBackend.Data;
using TestingBackend.Models;

namespace TestingBackend.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController
{
    private readonly AppDbContext _context;

    public UserController(AppDbContext context)
    {
        _context = context;
    }

    // [HttpPost]
    // public async Task<IActionResult> RegisterUser(User user)
    // {
    //     return Ok("User registered successfully.");
    // }
    

}
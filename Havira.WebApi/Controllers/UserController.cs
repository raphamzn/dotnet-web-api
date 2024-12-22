using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.ViewModels;

namespace WebApi.Controllers;

[ApiController]
[Route("v1")]
public class UserController : ControllerBase
{
    [HttpPost]
    [Route("users")]
    public async Task<IActionResult> PostAsync(
        [FromServices] AppDbContext context, CreateUserViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var existingUser = await context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Username == model.Username);
        if (existingUser != null)
            return BadRequest("Username already exists.");
        
        var user = new User
        {
            Username = model.Username,
            Password = model.Password
        };

        try
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
            return Created();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Services;
using WebApi.ViewModels;

namespace WebApi.Controllers;

[ApiController]
[Route("v1")]
public class LoginController : ControllerBase
{
    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> AuthenticateAsync(
        [FromServices] AppDbContext context, [FromBody] LoginViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = await context.Users.FirstOrDefaultAsync(x => 
            x.Username == model.Username && x.Password == model.Password);
        if (user == null)
            return NotFound(new { message = "Usuário ou senha inválidos" });

        user.Password = "";

        var token = TokenService.GenerateToken(user);
        return Ok(new { user = user, token = token });
    }   
}
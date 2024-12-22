using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Models;
using WebApi.ViewModels;

namespace WebApi.Controllers;

[ApiController]
[Route("v1")]
public class TodoController : ControllerBase
{
    [HttpGet]
    [Route("todos")]
    [Authorize]
    public async Task<IActionResult> GetAsync(
        [FromServices] AppDbContext context)
    {
        var todos = await context.Todos.AsNoTracking().Where(todo => 
            todo.UserId == User.GetUserId()).ToListAsync();
        return Ok(todos);
    }
    
    [HttpGet]
    [Route("todos/{id}")]
    [Authorize]
    public async Task<IActionResult> GetByIdAsync(
        [FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todo = await context.Todos.AsNoTracking().FirstOrDefaultAsync(x => 
            x.Id == id && x.UserId == User.GetUserId());
        return todo == null ? NotFound() : Ok(todo);
    }
    
    [HttpPost]
    [Route("todos")]
    [Authorize]
    public async Task<IActionResult> PostAsync(
        [FromServices] AppDbContext context, CreateTodoViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var todo = new Todo
        {
            Title = model.Title,
            Description = model.Description ?? "",
            CreatedAt = DateTime.Now,
            UserId = User.GetUserId()
        };

        try
        {
            await context.Todos.AddAsync(todo);
            await context.SaveChangesAsync();
            return Created($"v1/todos/{todo.Id}", todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpPut("todos/{id}")]
    [Authorize]
    public async Task<IActionResult> PutAsync(
        [FromServices] AppDbContext context, [FromRoute] int id, UpdateTodoViewModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id && x.UserId == User.GetUserId());
        if (todo == null)
            return NotFound();
        
        todo.Title = model.Title;
        todo.Description = model.Description;
        todo.CompletedAt = model.CompletedAt;

        try
        {
            context.Todos.Update(todo);
            await context.SaveChangesAsync();
            return Ok(todo);
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
    
    [HttpDelete("todos/{id}")]
    [Authorize]
    public async Task<IActionResult> DeleteAsync(
        [FromServices] AppDbContext context, [FromRoute] int id)
    {
        var todo = await context.Todos.FirstOrDefaultAsync(x => x.Id == id && x.UserId == User.GetUserId());
        if (todo == null)
            return NotFound();
        
        try
        {
            context.Todos.Remove(todo);
            await context.SaveChangesAsync();
            return NoContent();
        }
        catch (Exception e)
        {
            return BadRequest();
        }
    }
}
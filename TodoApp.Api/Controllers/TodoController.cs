using Microsoft.AspNetCore.Mvc;
using TodoApp.Api.Models;
using TodoApp.Api.Services;

namespace TodoApp.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _todoService;
    public TodoController(ITodoService todoService)
    {
        _todoService=todoService;
    }

    [HttpGet]
    public ActionResult<List<TodoItem>> GetAll()
    {
        var todos=_todoService.GetAll();
        return Ok(todos); //200
    }

    [HttpGet("{id:guid}")]
    public ActionResult<TodoItem> GetById(Guid id)
    {
        var todo=_todoService.GetbyId(id);
        if (todo is null) return NotFound();
        return Ok(todo);
    }

    [HttpPost]
    public ActionResult<TodoItem> Create(CreateTodoRequest request)
    {
        if (string.IsNullOrWhiteSpace(request.Title)) return BadRequest("Title ist erforderlich."); //400

        var todo=_todoService.Create(request.Title);
        return CreatedAtAction(nameof(GetById) , new {Id=todo.Id} , todo); //201
    }

    [HttpPut("{id:guid}/complete")]
    public IActionResult MarkAsComplated(Guid id)
    {
        var success=_todoService.MarkAsComplated(id);
        if (!success) return NotFound();
        return NoContent();
    }
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
       var success= _todoService.Delete(id);
       if (!success) return NotFound(); //404
       return NoContent();   //204
    }
}
//IActionResult : Diese Action kann verschiedene HTTP-Antworten zurückgeben
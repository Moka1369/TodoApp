using TodoApp.Api.Models;

namespace TodoApp.Api.Services;

public class TodoService : ITodoService
{
    private List<TodoItem> _todos = new();
    public List<TodoItem> GetAll()
    {
        return _todos;
    }
    public TodoItem? GetbyId(Guid id)
    {
        return _todos.FirstOrDefault(x => x.Id == id);
    }

    public TodoItem Create(string title)
    {
        var toDo = new TodoItem
        {
            Id = Guid.NewGuid(),
            Title = title,
            IsComplated = false,
            CreatedAt = DateTime.UtcNow
        };
        _todos.Add(toDo);
        return toDo;
    }
    public bool MarkAsComplated(Guid id)
    {
        var toDo = _todos.FirstOrDefault(x => x.Id == id);
        if (toDo is null) return false;
        toDo.IsComplated = true;
        return true;
    }
    public bool Delete(Guid id)
    {
        var toDo = _todos.FirstOrDefault(x => x.Id == id);
        if (toDo is null) return false;
        _todos.Remove(toDo);
        return true;
    }
}
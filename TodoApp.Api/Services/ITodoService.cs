using TodoApp.Api.Models;

namespace TodoApp.Api.Services;

public interface ITodoService
{
    List<TodoItem> GetAll();
    TodoItem? GetbyId(Guid id);
    TodoItem Create(string title);
    bool MarkAsComplated(Guid id);
    bool Delete(Guid id);

}
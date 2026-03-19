namespace TodoApp.Api.Models;

public class TodoItem
{
    public Guid Id { get; set; }
    public string Title { get; set; }=string.Empty;
    public bool IsComplated { get; set; }
    public DateTime CreatedAt { get; set; }
}

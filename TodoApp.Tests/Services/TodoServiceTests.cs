using TodoApp.Api.Services;

namespace TodoApp.Tests.Services;

public class TodoServiceTests
{
    [Fact]
    public void Create_Should_Add_New_Todo()
    {
        //Arrange
        var service = new TodoService();
        //Act
        var result = service.Create("Ich Lerne Github Action");

        //Assert
        Assert.NotNull(result);
        Assert.Equal("Ich Lerne Github Action", result.Title);
        Assert.False(result.IsComplated);
        Assert.NotEqual(Guid.Empty, result.Id);
        Assert.True(result.CreatedAt <= DateTime.UtcNow);
    }

    [Fact]
    public void GetAll_Should_Return_All_Created_Todos()
    {
        //Arrange
        var service = new TodoService();
        service.Create("Task 1");
        service.Create("Task 2");
        //Act
        var result = service.GetAll();
        //Assert
        Assert.Equal(2, result.Count);
        Assert.Contains(result, x => x.Title == "Task 1");
        Assert.Contains(result, x => x.Title == "Task 2");

    }

    [Fact]
    public void GetById_Should_Return_Todo_When_Id_Exists()
    {
        //Arrange
        var service = new TodoService();
        var created = service.Create("Test Todo");
        //Act
        var result = service.GetbyId(created.Id);
        //Assert
        Assert.NotNull(result);
        Assert.Equal(created.Id, result!.Id);
        Assert.Equal("Test Todo", result.Title);
    }

    [Fact]
    public void GetById_Should_Return_Null_When_Id_Does_Not_Exist()
    {
        //Arrange
        var service = new TodoService();
        //Act
        var result = service.GetbyId(Guid.NewGuid());
        //Assert
        Assert.Null(result);
    }
    [Fact]
    public void MarkAsCompleted_Should_Return_True_When_Todo_Exists()
    {
        //Arrange
        var service = new TodoService();
        var result = service.Create("Complete me");
        //Act
        var success = service.MarkAsComplated(result.Id);
        var updated = service.GetbyId(result.Id);
        //Assert
        Assert.True(success);
        Assert.NotNull(updated);
        Assert.True(updated!.IsComplated);
    }
    [Fact]
    public void MarkAsCompleted_Should_Return_False_When_Todo_Does_Not_Exist()
    {
        //Arrange
        var service = new TodoService();

        //Act
        var success = service.MarkAsComplated(Guid.NewGuid());
        //Assert
        Assert.False(success);
    }

    [Fact]
    public void Delete_Should_Return_True_When_ToDo_Exists()
    {
        //Arrange
        var service = new TodoService();
        var created = service.Create("Delete Task");
        //Act
        var success = service.Delete(created.Id);
        var deletedTodo = service.GetbyId(created.Id);
        //Assert
        Assert.True(success);
        Assert.Null(deletedTodo);
        Assert.Empty(service.GetAll());
    }
    [Fact]
    public void Delete_Should_Return_False_When_ToDo_Does_Not_Exists()
    {
        //Assert
        var service = new TodoService();
        //Act
        var success = service.Delete(Guid.NewGuid());
        //Assert
        Assert.False(success);
    }

}
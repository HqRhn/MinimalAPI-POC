using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoList.Models;

public interface ITodoRepository
{
    Task<List<TodoItem>> GetTodosAsync();
    Task<TodoItem> GetTodoAsync(int id);
    Task AddTodoAsync(TodoItem todoItem);
    Task UpdateTodoAsync(TodoItem todoItem);
    Task DeleteTodoAsync(int id);
}
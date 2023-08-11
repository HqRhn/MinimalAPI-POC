using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ToDoList.Models;

public class TodoRepository : ITodoRepository
{
    private readonly List<TodoItem> _todos = new List<TodoItem>();
    private int _nextId = 1;

    public async Task<List<TodoItem>> GetTodosAsync()
    {
        return await Task.FromResult(_todos);
    }

    public async Task<TodoItem> GetTodoAsync(int id)
    {
        return await Task.FromResult(_todos.FirstOrDefault(todo => todo.Id == id));
    }

    public async Task AddTodoAsync(TodoItem todoItem)
    {
        todoItem.Id = _nextId++;
        _todos.Add(todoItem);
        await Task.CompletedTask;
    }

    public async Task UpdateTodoAsync(TodoItem todoItem)
    {
        var existingTodo = _todos.FirstOrDefault(todo => todo.Id == todoItem.Id);
        if (existingTodo != null)
        {
            existingTodo.Title = todoItem.Title;
            existingTodo.IsDone = todoItem.IsDone;
        }
        await Task.CompletedTask;
    }

    public async Task DeleteTodoAsync(int id)
    {
        var existingTodo = _todos.FirstOrDefault(todo => todo.Id == id);
        if (existingTodo != null)
        {
            _todos.Remove(existingTodo);
        }
        await Task.CompletedTask;
    }
}

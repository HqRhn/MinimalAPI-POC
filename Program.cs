using ToDoList.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<ITodoRepository, TodoRepository>();

var app = builder.Build();

// Configure the app
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/todos", async (ITodoRepository repository) =>
{
    return Results.Ok(await repository.GetTodosAsync());
});

app.MapPost("/todos", async (ITodoRepository repository, TodoItem todoItem) =>
{
    await repository.AddTodoAsync(todoItem);
    return Results.Created($"/todos/{todoItem.Id}", todoItem);
});

app.MapGet("/todos/{id}", async (int id, ITodoRepository repository) =>
{
    var todoItem = await repository.GetTodoAsync(id);
    if (todoItem == null)
    {
        return Results.NotFound();
    }
    return Results.Ok(todoItem);
});

app.MapPut("/todos/{id}", async (int id, TodoItem todoItem, ITodoRepository repository) =>
{
    if (id != todoItem.Id)
    {
        return Results.BadRequest();
    }
    var existingTodo = await repository.GetTodoAsync(id);
    if (existingTodo == null)
    {
        return Results.NotFound();
    }
    await repository.UpdateTodoAsync(todoItem);
    return Results.Ok();
});

app.MapDelete("/todos/{id}", async (int id, ITodoRepository repository) =>
{
    var existingTodo = await repository.GetTodoAsync(id);
    if (existingTodo == null)
    {
        return Results.NotFound();
    }
    await repository.DeleteTodoAsync(id);
    return Results.NoContent();
});

app.Run();
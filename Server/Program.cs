using System;
using Microsoft.EntityFrameworkCore;
using Server.Data;
using Shared.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
//builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TodoDb>(opt => opt.UseInMemoryDatabase("Todo"));

var app = builder.Build();

app.UseCors(builder => builder.AllowAnyOrigin()
							 .AllowAnyHeader()
							.AllowAnyMethod());


app.MapGet("/todo/all", async (TodoDb dbContext) =>
{
	List<Todo> allTodo = await dbContext.tasks.ToListAsync();
	return allTodo;

}); ;

app.MapGet("/todo/complete", async (TodoDb dbContext) =>
{
	List<Todo> allTodo = await dbContext.tasks.Where(t => t.Done).ToListAsync();
	return allTodo;

}); ;

app.MapGet("/todo/tags", async (TodoDb dbContext) =>
{
	List<String> listtag = await dbContext.tasks.Select(t => t.Tegs).ToListAsync();
	return listtag;

}); ;

app.MapGet("todo/by-id/{TodoId}", async (int taskId, TodoDb dbContext) =>
{
	Todo? task = await dbContext.tasks.FindAsync(taskId);

	if (task != null)
	{
		return Results.Ok(task);
	}
	else
	{
		return Results.NotFound();
	}

}); 

app.MapPost("/todo/create", async (Todo task, TodoDb dbContext) => {

	dbContext.tasks.Add(task);

    await dbContext.SaveChangesAsync();

	return Results.Created($"todo/by-id/{task.TodoId}", task);
});

app.MapPut("/todo", async (Todo updateTask, TodoDb dbContext) => {

	var todo = await dbContext.tasks.FindAsync(updateTask.TodoId);

	if (todo is null) return Results.NotFound();

	todo.Name = updateTask.Name;
	todo.Desc = updateTask.Desc;
	todo.Tegs = updateTask.Tegs;
	todo.Done = updateTask.Done;

	await dbContext.SaveChangesAsync();

	return Results.NoContent();

});

app.MapDelete("/todo/{id}", async (int id, TodoDb dbContext) =>
{
	if (await dbContext.tasks.FindAsync(id) is Todo todo)
	{
		dbContext.tasks.Remove(todo);
		await dbContext.SaveChangesAsync();
		return Results.NoContent();
	}

	return Results.NotFound();
});

app.Run();

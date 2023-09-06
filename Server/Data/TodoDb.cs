using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace Server.Data;

public class TodoDb : DbContext
{
	public TodoDb(DbContextOptions<TodoDb> options) : base(options: options) { }

	public DbSet<Todo> tasks => Set<Todo>();

	protected override void OnModelCreating(ModelBuilder builder)
	{
		var taskToSeed = new Todo[6];

		for (int i =1; i<=6; i++)
		{
			taskToSeed[i - 1] = new Todo()
			{
				TodoId = i,
				Name = $"Задание {i}"
			};
		}

		builder.Entity<Todo>().HasData(taskToSeed);
	}
}


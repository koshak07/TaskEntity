using Microsoft.EntityFrameworkCore;

namespace TaskEntity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<EntityContext>(opt => opt.UseInMemoryDatabase("EntityTask"));
            var app = builder.Build();

            app.MapGet("/", () => "Hello World!");

            app.MapGet("/entity", async (EntityContext db) =>
            await db.Entityes.ToListAsync());

            app.MapGet("/entityes/{id}", async (Guid id, EntityContext db) =>
                await db.Entityes.FindAsync(id)
                    is Entity todo
                        ? Results.Ok(todo)
                        : Results.NotFound());

            app.MapPost("/entityes", async (Entity entity, EntityContext db) =>
            {
                db.Entityes.Add(entity);
                await db.SaveChangesAsync();

                return Results.Created($"/todoitems/{entity.Id}", entity);
            });

            app.Run();
        }
    }
}
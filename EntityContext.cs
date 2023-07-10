using Microsoft.EntityFrameworkCore;

namespace TaskEntity
{
    internal class EntityContext : DbContext
    {
        public EntityContext(DbContextOptions<EntityContext> options)
            : base(options) { }

        public DbSet<Entity> Entityes => Set<Entity>();
    }
}
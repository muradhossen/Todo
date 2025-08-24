using Domain.Entities.Todos;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistance
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        public DbSet<Todo> Todos { get; set; }
    }
}

using Domain.Entities.Tasks;
using Domain.Entities.Users;
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
        public DbSet<User> Users { get; set; }
        


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        { 
            // Task -> User (many-to-one)
            modelBuilder.Entity<Todo>()
                .HasOne(t => t.AssignToUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignToUserId);
        }
    }
}

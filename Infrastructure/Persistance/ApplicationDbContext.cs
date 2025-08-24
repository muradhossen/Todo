using Domain.Entities.Teams;
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

        public DbSet<Domain.Entities.Tasks.Task> Tasks { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Team> Teams { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // User -> Team (many-to-one)
            modelBuilder.Entity<User>()
                .HasOne(u => u.Team)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.TeamId);

            // Task -> Team (many-to-one)
            modelBuilder.Entity<Domain.Entities.Tasks.Task>()
                .HasOne(t => t.Team)
                .WithMany(team => team.Tasks)
                .HasForeignKey(t => t.TeamId);

            // Task -> User (many-to-one)
            modelBuilder.Entity<Domain.Entities.Tasks.Task>()
                .HasOne(t => t.AssignToUser)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.AssignToUserId);
        }
    }
}

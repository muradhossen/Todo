using Application.RepositoryInterfaces;
using Application.RepositoryInterfaces.Users;
using Infrastructure.Persistance;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DIContainer
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(option =>
            {
                option.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });


            services.AddScoped<ITaskRepository, TodoRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ITeamRepository, TeamRepository>();

            return services;
        }
    }
}

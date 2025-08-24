using Application.ServiceInterfaces;
using Application.ServiceInterfaces.Token;
using Application.ServiceInterfaces.Users;
using Application.Services;
using Application.Services.Token;
using Application.Services.Users;
using Application.Validations;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DIContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ITaskService, TaskService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ITeamService, TeamService>();

            services.AddValidatorsFromAssemblyContaining<UserCreateDTOValidator>();


            return services;
        }

    }
}

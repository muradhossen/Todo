using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DIContainer
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        { 
            return services;
        }

    }
}

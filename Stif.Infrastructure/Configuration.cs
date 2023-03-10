using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Stix.Infrastructure
{
    public static class Configuration
    {
        public static IServiceCollection AddEntityFrameworkCoreSqlServer(this IServiceCollection services, string? sqlConnectionString)
        {
            services.AddDbContext<StixDbContext>(options => options.UseSqlServer(sqlConnectionString));


            return services;
        }
    }
}
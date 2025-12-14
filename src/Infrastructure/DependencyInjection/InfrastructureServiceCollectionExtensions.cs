using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.DependencyInjection;

public static class InfrastructureServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContextPool<ServiceDbContext>(o => 
                o.UseSqlServer(configuration.GetConnectionString(""), b => b.EnableRetryOnFailure()), 256)
            .AddPooledDbContextFactory<ServiceDbContext>(o =>
                o.UseSqlServer(configuration.GetConnectionString(""), b => b.EnableRetryOnFailure()), 256);

        return services;
    }
}

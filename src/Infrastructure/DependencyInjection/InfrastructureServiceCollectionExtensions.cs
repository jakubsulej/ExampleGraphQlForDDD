using Domain.Aggregates.BookingAggregate.Repositories;
using Domain.Aggregates.BookingReviewAggregate.Repositories;
using Domain.Aggregates.CleanerAggregate.Repositories;
using Domain.Aggregates.CustomerAggregate.Repositories;
using Domain.Aggregates.ServiceOfferAggregate.Repositories;
using Infrastructure.EntityFramework;
using Infrastructure.EntityFramework.Repositories;
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
                o.UseSqlServer(configuration.GetConnectionString(""), b => b.EnableRetryOnFailure()), 256)
            .AddScoped<IBookingQueryStore, BookingQueryStore>()
            .AddScoped<IBookingReviewQueryStore, BookingReviewQueryStore>()
            .AddScoped<ICleanerQueryStore, CleanerQueryStore>()
            .AddScoped<ICustomerQueryStore, CustomerQueryStore>()
            .AddScoped<IServiceOfferQueryStore, ServiceOfferQueryStore>();

        return services;
    }
}

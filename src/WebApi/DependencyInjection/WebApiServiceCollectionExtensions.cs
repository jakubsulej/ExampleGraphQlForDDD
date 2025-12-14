namespace WebApi.DependencyInjection;

public static class WebApiServiceCollectionExtensions
{
    public static IServiceCollection AddWebApiServices(this IServiceCollection services, IConfiguration configuration)
    { 
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .DisableIntrospection(false)
            .ModifyCostOptions(o =>
            {
                o.EnforceCostLimits = true;
                o.MaxFieldCost = 1_000;
                o.MaxTypeCost = 2_000;
            })
            .RemoveMaxAllowedFieldCycleDepthRule()
            .AddMaxAllowedFieldCycleDepthRule(defaultCycleLimit: 6);

        return services;
    }
}

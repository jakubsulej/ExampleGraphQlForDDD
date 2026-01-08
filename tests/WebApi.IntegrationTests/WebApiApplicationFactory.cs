using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Testcontainers.MsSql;

namespace WebApi.IntegrationTests;

public class WebApiApplicationFactory : WebApplicationFactory<Program>
{
    private readonly MsSqlContainer _msSqlContainer;

    public WebApiApplicationFactory(MsSqlContainer msSqlContainer)
    {
        _msSqlContainer = msSqlContainer;
    }

    protected override IHost CreateHost(IHostBuilder builder)
    {
        var initialData = new Dictionary<string, string>
        {
            ["ASPNETCORE_ENVIRONMENT"] = "Development",
            ["ConnectionStrings:ServiceDatabase"] = _msSqlContainer.GetConnectionString(),
        };

        builder.ConfigureAppConfiguration((_, configBuilder) =>
        {
            configBuilder.Sources.Clear();
            configBuilder.AddInMemoryCollection(initialData!);
        });

        builder.ConfigureLogging(logging =>
        {
            logging
                .ClearProviders()
                .AddConsole()
                .AddDebug();
        });

        return base.CreateHost(builder);
    }
}

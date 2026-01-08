using Infrastructure.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace WebApi.IntegrationTests;

[CollectionDefinition(Name)]
public class ApplicationTestCollection : ICollectionFixture<ApplicationTestFixture>
{
    public const string Name = "Application test collection";
}

public class ApplicationTestFixture : IAsyncLifetime
{
    private static readonly int ContainerSuffix = Random.Shared.Next(0, 5000);

    private readonly MsSqlContainer _msSqlTestContainer = new(new MsSqlConfiguration(
        database: $"test-sql-{ContainerSuffix}", password: "Some@Passw0rd"));

    public WebApiApplicationFactory WebApiApplicationFactory { get; private set; }

    public ApplicationTestFixture()
    {
        WebApiApplicationFactory = new WebApiApplicationFactory(_msSqlTestContainer);
    }

    public async Task InitializeAsync()
    {
        var mssqlTask = _msSqlTestContainer.StartAsync();
        await Task.WhenAll(mssqlTask);

        var server = WebApiApplicationFactory.Server;

        using var scope = server.Services.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ServiceDbContext>();
        await dbContext.Database.MigrateAsync();
    }

    public async Task DisposeAsync()
    {
        await _msSqlTestContainer.DisposeAsync();
    }
}

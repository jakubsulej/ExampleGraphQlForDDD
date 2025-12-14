using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using WebApi.DependencyInjection;

internal class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        var app = builder.Build();

        builder.Services.AddWebApiServices(configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(configuration);

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseAuthentication();

        app.MapGraphQL();

        app.Run();
    }
}
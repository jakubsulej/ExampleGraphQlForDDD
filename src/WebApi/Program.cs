using Application.DependencyInjection;
using Infrastructure.DependencyInjection;
using WebApi.DependencyInjection;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var configuration = builder.Configuration;

        //builder.Services
        //    .AddAuthentication("Bearer")
        //    .AddJwtBearer("Bearer", object => 
        //    {

        //    });
        //builder.Services.AddAuthorization();

        builder.Services.AddWebApiServices(configuration);
        builder.Services.AddApplicationServices();
        builder.Services.AddInfrastructureServices(configuration);

        var app = builder.Build();

        app.UseHttpsRedirection();
        //app.UseAuthorization();
        //app.UseAuthentication();

        app.MapGraphQL();

        app.Run();
    }
}
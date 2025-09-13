using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using VehicleCatalog.Domain.Interfaces;
using VehicleCatalog.Application.Mappings;
using System.Reflection;
using VehicleCatalog.Infrastructure.Data;
using VehicleCatalog.Infrastructure.Repositories;

namespace VehicleCatalog.Tests;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Test.json", optional: true, reloadOnChange: true);
        
        Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // DbContext com InMemory
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString());
            options.EnableSensitiveDataLogging();
            options.EnableDetailedErrors();
        });

        // Repositories e Unit of Work 
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IVehicleRepository, VehicleRepository>();

        // AutoMapper 
        services.AddAutoMapper(typeof(MappingProfile).Assembly);

        // MediatR 
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.Load("VehicleCatalog.Application"));
        });

        // Logging
        services.AddLogging();

        // Configuration
        services.AddSingleton<IConfiguration>(Configuration);
    }
}

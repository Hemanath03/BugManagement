using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Modules.BugManagement.Bugs.Features.Create;
using Modules.BugManagement.Shared.Data.Context;
using Modules.BugManagement.Shared.Data.Repositories;
using Shared.Abstractions.Data;

namespace Modules.BugManagement;

public static class DependencyInjection
{
    public static IServiceCollection AddBugManagementModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBugDb(configuration);
        services.AddMediatorServices();
        services.AddRepositories();

        return services;
    }

    // ---------------- DB CONFIG ----------------
    private static IServiceCollection AddBugDb(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("BugDb");

        if (!string.IsNullOrWhiteSpace(connectionString))
        {
            services.AddDbContext<BugDbContext>(options =>
                options.UseMySql(
                        connectionString,
                        serverVersion: ServerVersion.AutoDetect(connectionString),
                    sql =>
                    {
                        sql.MigrationsAssembly(
                            typeof(BugDbContext).Assembly.FullName);
                    }),
                ServiceLifetime.Transient);
        }

        // abstraction mapping (VERY IMPORTANT)
        services.AddScoped<IApplicationDbContext>(
            sp => sp.GetRequiredService<BugDbContext>());

        return services;
    }

    // ---------------- MEDIATOR ----------------
    private static IServiceCollection AddMediatorServices(
        this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(DependencyInjection).Assembly));

        services.AddValidatorsFromAssembly(
            typeof(DependencyInjection).Assembly);

        return services;
    }

    // ---------------- REPOSITORIES ----------------
    private static IServiceCollection AddRepositories(
        this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateBugValidator>();
        services.AddTransient<BugRepository>();

        return services;
    }
}

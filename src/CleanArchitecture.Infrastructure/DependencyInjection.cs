using System;
using CleanArchitecture.Application.Abstractions.Caching;
using CleanArchitecture.Application.Abstractions.Common;
using CleanArchitecture.Application.Abstractions.Data;
using CleanArchitecture.Application.Abstractions.Email;
using CleanArchitecture.Application.Abstractions.Messaging;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.BackgroundServices;
using CleanArchitecture.Infrastructure.Data.DataContext;
using CleanArchitecture.Infrastructure.Data.Interceptors;
using CleanArchitecture.Infrastructure.Data.Repositories;
using CleanArchitecture.Infrastructure.HealthChecks;
using CleanArchitecture.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchitecture.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services, 
        IConfiguration configuration)
    {
        // Register Interceptors
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DomainEventDispatcherInterceptor>();

        // Database
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName))
                .AddInterceptors(interceptors);
        });

        services.AddScoped<IApplicationDbContext>(sp => 
            sp.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IUnitOfWork>(sp => 
            new UnitOfWork(sp.GetRequiredService<ApplicationDbContext>()));

        // Repositories
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<ICustomerRepository, CustomerRepository>();

        // Common Services
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddHttpContextAccessor();

        // Application Services
        services.AddScoped<IEmailService, EmailService>();
        services.AddSingleton<ICacheService, CacheService>();
        services.AddScoped<IEventBus, EventBus>();

        // Background Services
        services.AddHostedService<CacheCleanupService>();
        services.AddHostedService<DomainEventProcessorService>();

        // Health Checks
        services.AddHealthChecks()
            .AddCheck<DatabaseHealthCheck>("database", tags: new[] { "db", "sql" })
            .AddCheck<CacheHealthCheck>("cache", tags: new[] { "cache", "memory" })
            .AddDbContextCheck<ApplicationDbContext>("ef-dbcontext", tags: new[] { "db", "ef" });

        return services;
    }

}

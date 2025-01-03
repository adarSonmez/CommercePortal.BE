﻿using CommercePortal.Application.Abstractions.Services.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Application.Extensions;

/// <summary>
/// Extension methods for the <see cref="IApplicationBuilder"/> interface.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// Migrates the database to the latest version.
    /// </summary>
    public static void UseMigrator(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var migrator = services.GetRequiredService<IDataService>();
        migrator.Migrate();
    }

    /// <summary>
    /// Seeds the database with initial data.
    /// </summary>
    public static void UseSeeder(this IApplicationBuilder builder)
    {
        using var scope = builder.ApplicationServices.CreateScope();
        var services = scope.ServiceProvider;
        var seeder = services.GetRequiredService<IDataService>();
        seeder.Seed();
    }
}
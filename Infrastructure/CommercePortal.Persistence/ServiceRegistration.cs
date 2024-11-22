﻿using CommercePortal.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CommercePortal.Persistence
{
    /// <summary>
    /// Represents the service registration for the persistence layer.
    /// </summary>
    public static class ServiceRegistration
    {
        /// <summary>
        /// Adds the persistence services to the service collection.
        /// </summary>
        /// <param name="services">The service collection to add the services to.</param>
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            services.AddDbContext<EfDbContext>(options =>
            {
                options.UseNpgsql("Host=localhost;Port=5432;Database=CommercePortal;Username=postgres;Password=****");
            });
        }
    }
}
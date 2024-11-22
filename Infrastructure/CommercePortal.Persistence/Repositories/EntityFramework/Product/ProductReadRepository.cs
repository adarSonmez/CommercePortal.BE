﻿using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents the <see cref="Product"/> read repository.
    /// </summary>
    public class ProductReadRepository : EfReadRepository<Product, EfDbContext>, IProductReadRepository
    {
    }
}
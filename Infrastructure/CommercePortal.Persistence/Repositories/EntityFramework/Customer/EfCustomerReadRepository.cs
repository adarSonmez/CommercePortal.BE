﻿using CommercePortal.Application.Repositories;
using CommercePortal.Domain.Entities;
using CommercePortal.Persistence.Contexts;

namespace CommercePortal.Persistence.Repositories.EntityFramework
{
    /// <summary>
    /// Represents EntityFramework implementation of the <see cref="Customer"/> read repository.
    /// </summary>
    public class EfCustomerReadRepository(EfDbContext context) : EfReadRepository<Customer>(context), ICustomerReadRepository
    {
    }
}
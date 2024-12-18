﻿using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories.Customers;

/// <summary>
/// Represents the read repository interface for the <see cref="Customer"/> entity.
/// </summary>
public interface ICustomerReadRepository : IReadRepository<Customer>
{
}
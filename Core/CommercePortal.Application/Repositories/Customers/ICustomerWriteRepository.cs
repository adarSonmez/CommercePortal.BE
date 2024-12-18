﻿using CommercePortal.Domain.Entities;

namespace CommercePortal.Application.Repositories.Customers;

/// <summary>
/// Represents the write repository interface for the <see cref="Customer"/> entity.
/// </summary>
public interface ICustomerWriteRepository : IWriteRepository<Customer>
{
}
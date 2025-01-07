﻿using CommercePortal.Domain.Constants.Enums;
using CommercePortal.Domain.Entities.Ordering;
using CommercePortal.Domain.ValueObjects;

namespace CommercePortal.Application.Dtos.Ordering;

/// <summary>
/// Represents data transfer object for <see cref="Invoice"/>
/// </summary>
/// <param name="Id">The unique identifier.</param>
/// <param name="BillingAddress">The billing address of the invoice.</param>
/// <param name="OrderId">The identifier of the order associated with the invoice.</param>s
/// <param name="IssuedAt">The date and time when the invoice was issued.</param>
/// <param name="DueDate">The due date of the invoice.</param>
/// <param name="Status">The status of the invoice.</param>
/// <param name="PaymentMethod">The payment method of the invoice.</param>
/// <param name="TransactionId">The transaction id of the invoice.</param>
/// <param name="Notes">The notes of the invoice.</param>
/// <param name="PaidAt">The date and time when the invoice was paid.</param>
public record InvoiceDto
(
    Guid Id,
    Address BillingAddress,
    Guid OrderId,
    DateTime IssuedAt,
    DateTime DueDate,
    InvoiceStatus Status,
    PaymentMethod PaymentMethod,
    string? TransactionId,
    string? Notes,
    DateTime? PaidAt
);
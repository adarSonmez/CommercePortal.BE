﻿using CommercePortal.Domain.Common;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents an invoice file entity.
/// </summary>
public class InvoiceFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the foreign key for the FileDetails.
    /// </summary>
    public Guid FileDetailsId { get; set; }

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}
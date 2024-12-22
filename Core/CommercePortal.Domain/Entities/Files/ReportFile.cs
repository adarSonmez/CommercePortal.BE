﻿using CommercePortal.Domain.Common;
using CommercePortal.Domain.Constants.Enums;

namespace CommercePortal.Domain.Entities.Files;

/// <summary>
/// Represents a report file entity.
/// </summary>
public class ReportFile : BaseEntity
{
    /// <summary>
    /// Gets or sets the report type associated with the file.
    /// </summary>
    public ReportType ReportType { get; set; }

    /// <summary>
    /// Gets or sets the file details.
    /// </summary>
    public FileDetails FileDetails { get; set; } = default!;
}
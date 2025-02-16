﻿using CleanArchitectureTemplate.Domain.Entities.Files;

namespace CleanArchitectureTemplate.Application.Abstractions.Repositories.Files;

/// <summary>
/// Represents the read repository interface for the <see cref="ReportFile"/> entity.
/// </summary>
public interface IReportFileWriteRepository : IWriteRepository<ReportFile>
{
}
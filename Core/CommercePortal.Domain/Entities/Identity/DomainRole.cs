﻿using CommercePortal.Domain.MarkerInterfaces;

/**
 * Domain Entities Overview:
 *
 * Domain entities encapsulate core business rules and logic, independent of infrastructure or external systems, adhering to clean architecture principles.
 *
 * Key Features:
 * - **Independence:** Decoupled from identity frameworks or infrastructure for flexibility and testability.
 * - **Behavior-Focused:** Prioritize business logic over mere data storage.
 * - **No Base Inheritance:** `DomainRole` avoids `EntityBase` to stay infrastructure-agnostic.
 *
 * About `DomainRole`:
 * - Represents roles in the domain layer, abstracted from external identity systems.
 * - Decoupled from persistence or identity specifics, with mappings handled in other layers.
 */

namespace CommercePortal.Domain.Entities.Identity;

/// <summary>
/// Represents a role within the domain layer, independent of any external identity systems.
/// </summary>
public class DomainRole : IEntity
{
    private Guid? _id;

    /// <summary>
    /// Gets or sets the unique identifier for the role.
    /// If not explicitly set, a new GUID is generated.
    /// </summary>
    public required Guid Id
    {
        get => _id ??= Guid.NewGuid();
        set => _id = value;
    }

    /// <summary>
    /// Gets or sets the name of the role.
    /// </summary>
    public required string Name { get; set; }
}
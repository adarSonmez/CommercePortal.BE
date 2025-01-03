﻿using CommercePortal.Domain.MarkerInterfaces;

namespace CommercePortal.Application.DTOs;

/// <summary>
/// Represents the token data transfer object.
/// </summary>
/// <param name="AccessToken">The access token.</param>
/// <param name="ExpirationTime">The expiration time of the token.</param>
/// <param name="RefreshToken">The refresh token.</param>
public record TokenDTO
(
    string? AccessToken,
    DateTime ExpirationTime,
    string? RefreshToken
) : IDto;
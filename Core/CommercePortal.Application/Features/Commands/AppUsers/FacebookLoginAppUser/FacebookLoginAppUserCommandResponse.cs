﻿using CommercePortal.Application.DTOs;

namespace CommercePortal.Application.Features.Commands.AppUsers.FacebookLoginAppUser;

/// <summary>
/// Represents the response of the <see cref="FacebookLoginAppUserCommandRequest"/>.
/// </summary>
/// <param name="Token">The token generated for the user.</param>
public record FacebookLoginAppUserCommandResponse(TokenDTO Token);
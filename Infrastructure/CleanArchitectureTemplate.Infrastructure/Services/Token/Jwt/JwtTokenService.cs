﻿using CleanArchitectureTemplate.Application.Abstractions.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;
using DTO = CleanArchitectureTemplate.Application.DTOs;

namespace CleanArchitectureTemplate.Infrastructure.Services.Token.Jwt;

/// <summary>
/// Represents a handler for generating and validating JWT tokens.
/// </summary>
public class JwtTokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    /// </inheritdoc>
    public DTO::TokenDTO GenerateToken(Guid userId, bool? infiniteExpiration = false)
    {
        var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]!));
        var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

        var expirationDate = infiniteExpiration == true
            ? DateTime.MaxValue
            : DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:AccessTokenExpiration"]));

        var jwtSecurityToken = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            expires: expirationDate,
            notBefore: DateTime.Now,
            signingCredentials: signingCredentials
        // claims: SetClaims(userId.ToString(), username, email, role)
        );

        var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        var accessToken = jwtSecurityTokenHandler.WriteToken(jwtSecurityToken);
        var refreshToken = GenerateRefreshToken();

        return new DTO::TokenDTO(accessToken, expirationDate, refreshToken);
    }

    /// <summary>
    /// Generates a refresh token to be used for refreshing the access token.
    /// </summary>
    /// <returns>The generated refresh token.</returns>
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[128];
        using (var rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);

            for (var i = 0; i < randomNumber.Length; i++)
            {
                Console.Write(randomNumber[i]);
            }

            return Convert.ToBase64String(randomNumber);
        }
    }
}
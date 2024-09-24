﻿using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackEnd.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Interfaces;

public class JwtProvider:IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _options.SecretKey = GenerateSecretKey();
    }
    
    public string GenerateToken(LoginInfo loginInfo, string[] permissions)
    {
        var claims = new List<Claim>
        {
            new Claim("loginInfoId", loginInfo.Id.ToString()),
            new Claim("login", loginInfo.Login)
        };
        
        // Додаємо дозволи до claims
        foreach (var permission in permissions)
        {
            claims.Add(new ("Permission", permission));
        }
        
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(_options.SecretKey), SecurityAlgorithms.HmacSha256);
        
        var token = new JwtSecurityToken(
            claims: claims,
            signingCredentials: signingCredentials,
            expires: DateTime.UtcNow.AddHours(_options.ExpiredHours)
        );

        var tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
        
        return tokenValue;
    }

    public byte[] GenerateSecretKey()
    {
        using (var sha256 = SHA256.Create())
        {
            return sha256.ComputeHash(Encoding.UTF8.GetBytes(NetworkInterface
                .GetAllNetworkInterfaces()
                .Where( nic => nic.OperationalStatus == OperationalStatus.Up && nic.NetworkInterfaceType != NetworkInterfaceType.Loopback )
                .Select( nic => nic.GetPhysicalAddress().ToString() )
                .FirstOrDefault() ?? string.Empty));
        }
    }
    
}
using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BackEnd.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BackEnd.Infrastructure;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _options;

    public JwtProvider(IOptions<JwtOptions> options)
    {
        _options = options.Value;
        _options.SecretKey = GenerateSecretKey();
    }

    public string GenerateToken(LoginInfo? loginInfo)
    {
        var claims = new List<Claim>
        {
            new Claim(CustomClaims.UserId, loginInfo?.UserId.ToString() ?? string.Empty),
        };

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
        return SHA256.HashData(Encoding.UTF8.GetBytes(NetworkInterface
            .GetAllNetworkInterfaces()
            .Where(nic =>
                nic.OperationalStatus == OperationalStatus.Up &&
                nic.NetworkInterfaceType != NetworkInterfaceType.Loopback)
            .Select(nic => nic.GetPhysicalAddress().ToString())
            .FirstOrDefault() ?? string.Empty));
    }

}
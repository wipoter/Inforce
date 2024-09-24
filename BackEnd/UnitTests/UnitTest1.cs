using System.IdentityModel.Tokens.Jwt;
using System.Net.NetworkInformation;
using System.Security.Claims;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;

namespace UnitTests;

public class JwtProviderTests
{
    private JwtProvider _jwtProvider;
    private Mock<IOptions<JwtOptions>> _optionsMock;
    private JwtOptions _jwtOptions;

    [SetUp]
    public void Setup()
    {
        _jwtOptions = new JwtOptions
        {
            ExpiredHours = 1,
        };

        _optionsMock = new Mock<IOptions<JwtOptions>>();
        _optionsMock.Setup(o => o.Value).Returns(_jwtOptions);

        _jwtProvider = new JwtProvider(_optionsMock.Object);
    }

    [Test]
    public void GenerateToken_ReturnsValidToken()
    {
        // Arrange
        var loginInfo = LoginInfo.Create(Guid.NewGuid(), "login", "passwordHash"); // Ініціалізуйте необхідні параметри логіна
        
        // Act
        var token = _jwtProvider.GenerateToken(loginInfo);
        
        // Assert
        Assert.IsNotNull(token);
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        Assert.IsNotNull(jwtToken);
        Assert.That(jwtToken.SignatureAlgorithm, Is.EqualTo(SecurityAlgorithms.HmacSha256));
        Console.WriteLine(jwtToken);
        Console.WriteLine();
    }
}
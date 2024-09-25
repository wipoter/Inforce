using BackEnd.Models;

namespace BackEnd.Infrastructure;

public interface IJwtProvider
{
    string GenerateToken(LoginInfo? loginInfo);
    byte[] GenerateSecretKey();
}
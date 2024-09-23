using BackEnd.Models;

namespace BackEnd.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(LoginInfo loginInfo);
    byte[] GenerateSecretKey();
}
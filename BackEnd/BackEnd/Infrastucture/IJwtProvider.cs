using BackEnd.Data;
using BackEnd.Models;

namespace BackEnd.Interfaces;

public interface IJwtProvider
{
    string GenerateToken(LoginInfo loginInfo, string[] permissions);
    byte[] GenerateSecretKey();
}
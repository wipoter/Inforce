namespace BackEnd.Services;

public interface ILoginInfoService
{
    Task Register(string login, string password);
    Task<string> Login(string login, string password);
}
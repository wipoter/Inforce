namespace BackEnd.Models;

public class LoginInfo(Guid id, string login, string passwordHash)
{
    public Guid Id { get; set; } = id;
    public string Login { get; private set; } = login;
    public string PasswordHash { get; private set; } = passwordHash;
    public Guid UserId { get; set; }
}
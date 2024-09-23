namespace BackEnd.Models;

public class LoginInfo
{
    private LoginInfo(Guid id, string login, string passwordHash)
    {
        Id = id;
        Login = login;
        PasswordHash = passwordHash;
    }

    public Guid Id { get; set; }
    public string Login { get; private set; }
    public string PasswordHash { get; private set; }

    public Guid UserId { get; set; } // Змінено на Guid

    public static LoginInfo Create(Guid id, string login, string passwordHash) => new LoginInfo(id, login, passwordHash);
}
namespace BackEnd.Infrastructure;

public class JwtOptions
{
    public byte[]? SecretKey { get; set; }
    public int ExpiredHours { get; set; }
}
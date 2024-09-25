namespace BackEnd.Infrastructure;

public class UrlInfoRequest(string longUrl)
{
    public string LongUrl { get; set; } = longUrl;
}
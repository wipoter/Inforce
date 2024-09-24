namespace BackEnd.Models;

public class UrlInfo
{
    private UrlInfo(Guid id, string longUrl, string shortUrl)
    {
        Id = id;
        LongUrl = longUrl;
        ShortUrl = shortUrl;
    }

    public Guid Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public Guid? UserId { get; set; }

    public static UrlInfo Create(Guid id, string longUrl, string shortUrl) =>
        new UrlInfo(id, longUrl, shortUrl);
}
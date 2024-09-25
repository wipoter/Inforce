namespace BackEnd.Models;

public class UrlInfo(Guid id, string longUrl, string shortUrl, string createdBy)
{
    public Guid Id { get; set; } = id;
    public string LongUrl { get; set; } = longUrl;
    public string ShortUrl { get; set; } = shortUrl;
    public string CreatedBy { get; set; } = createdBy;
    public DateTime CreatedDate { get; set; }
}
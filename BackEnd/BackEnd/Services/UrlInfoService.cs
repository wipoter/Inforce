using BackEnd.Entities;
using BackEnd.Models;
using BackEnd.Repositories;

namespace BackEnd.Services;

public class UrlInfoService(IUrlInfoRepository infoRepository):IUrlInfoService
{
    public Task<IEnumerable<UrlInfo>> GetAllUrlInfo()
    {
        return infoRepository.GetAllUrlInfo();
    }
    public async Task AddUrlInfoAsync(Guid userId, string longUrl)
    {
        var urlInfo = new UrlInfo(Guid.NewGuid(), longUrl, await ShortenUrlAsync(longUrl), string.Empty);
        await infoRepository.CreateUrlInfoAsync(userId, urlInfo);
    }

    public async Task DeleteInfoAsync(Guid userId, Guid urlInfoId)
    {
        await infoRepository.DeleteAsync(userId, urlInfoId);
    }

    public async Task<UrlInfoEntity?> GetUrlInfoByIdAsync(Guid urlInfoId)
    {
        return await infoRepository.GetUrlInfoByIdAsync(urlInfoId);
    }

    private Task<string> ShortenUrlAsync(string originalUrl)
    {
        //Improvisation
        var shortUrl = Convert.ToBase64String(Guid.NewGuid().ToByteArray())
            .Substring(0, 8)
            .Replace("/", "-")
            .Replace("+", "_");
        
        return Task.FromResult(shortUrl);
    }
}
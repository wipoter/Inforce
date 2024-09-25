using BackEnd.Entities;
using BackEnd.Models;

namespace BackEnd.Services;

public interface IUrlInfoService
{
    Task AddUrlInfoAsync(Guid userId, string longUrl);
    Task DeleteInfoAsync(Guid userId, Guid urlInfoId);
    Task<UrlInfoEntity?> GetUrlInfoByIdAsync(Guid urlInfoId);
    Task<IEnumerable<UrlInfo>> GetAllUrlInfo();
}
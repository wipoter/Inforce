using BackEnd.Entities;
using BackEnd.Models;

namespace BackEnd.Repositories;

public interface IUrlInfoRepository
{
    Task CreateUrlInfoAsync(Guid userId, UrlInfo urlInfo);

    Task DeleteAsync(Guid userId, Guid urlInfoId);

    Task<UrlInfoEntity?> GetUrlInfoByIdAsync(Guid urlInfoId);

    Task<IEnumerable<UrlInfo>> GetAllUrlInfo();
}
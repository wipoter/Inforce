using AutoMapper;
using BackEnd.Data;
using BackEnd.Entities;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Repositories;

public class UrlInfoRepository(ShortenerUrlContext context, IMapper mapper):IUrlInfoRepository
{

    public Task<IEnumerable<UrlInfo>> GetAllUrlInfo()
    {
        return Task.FromResult<IEnumerable<UrlInfo>>(context.UrlInfos.Select(u => mapper.Map<UrlInfo>(u)));
    }
    public async Task CreateUrlInfoAsync(Guid userId, UrlInfo urlInfo)
    {
        var user = await context.Users.FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return;

        var login = await context.LoginInfos.Where(l => l.UserId == userId).Select(l => l.Login).FirstOrDefaultAsync();

        if (login == null)
            return;
        
        var urlInfoEntity = new UrlInfoEntity()
        {
            Id = Guid.NewGuid(),
            CreatedBy = login,
            CreatedDate = DateTime.Now,
            LongUrl = urlInfo.LongUrl,
            ShortUrl = urlInfo.ShortUrl,
            UserId = userId 
        };

        await context.UrlInfos.AddAsync(urlInfoEntity);
        user.UrlInfos.Add(urlInfoEntity);
        await context.SaveChangesAsync();

    }

    public async Task DeleteAsync(Guid userId, Guid urlInfoId)
    {
        if (userId == default)
        {
            var urlInfo = await context.UrlInfos.FindAsync(urlInfoId);
            if (urlInfo != null) context.UrlInfos.Remove(urlInfo);
        }
        else
        {
            var urlInfos = await context.Users
                .Where(u => u.Id == userId).Select(u => u.UrlInfos).FirstOrDefaultAsync();

            var urlInfo = urlInfos?.FirstOrDefault(u => u.Id == urlInfoId);

            if (urlInfo != null)
                context.UrlInfos.Remove(urlInfo);
        }
        
        await context.SaveChangesAsync();
    }

    public async Task<UrlInfoEntity?> GetUrlInfoByIdAsync(Guid urlInfoId)
    {
        return await context.UrlInfos.Where(u => u.Id == urlInfoId).FirstOrDefaultAsync();
    }
}
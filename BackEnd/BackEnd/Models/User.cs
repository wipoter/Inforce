using BackEnd.Entities;

namespace BackEnd.Models;

public class User(Guid id, int loginInfoId)
{
    public Guid Id { get; set; } = id;
    public ICollection<UrlInfoEntity> UrlInfos { get; set; } = new List<UrlInfoEntity>();

    public int? LoginInfoId { get; set; } = loginInfoId;
    public Guid? UserId { get; set; }
}
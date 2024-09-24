namespace BackEnd.Models;

public class User
{
    private User(Guid id, int loginInfoId)
    {
        Id = id;
        UrlInfos = new List<UrlInfoEntity>();
        LoginInfoId = loginInfoId;
    }

    public Guid Id { get; set; }
    public virtual ICollection<UrlInfoEntity> UrlInfos { get; set; }

    public int? LoginInfoId { get; set; }
    public Guid? UserId { get; set; }
}
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

public class UserEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    
    // Ініціалізована колекція
    public virtual ICollection<UrlInfoEntity> UrlInfos { get; set; } = new List<UrlInfoEntity>();

    public Guid? LoginInfoId { get; set; }
    public virtual LoginInfoEntity? LoginInfo { get; set; }

    public ICollection<RoleEntity> Roles { get; set; } = new List<RoleEntity>();
}
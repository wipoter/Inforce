using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

public class LoginInfoEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    
    public Guid? UserId { get; set; }
    public virtual UserEntity? User { get; set; }
}
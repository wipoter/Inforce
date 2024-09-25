using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Entities;

public sealed class LoginInfoEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string? Login { get; set; }
    public string? PasswordHash { get; set; }
    
    public Guid? UserId { get; set; }
    public UserEntity? User { get; set; }
}
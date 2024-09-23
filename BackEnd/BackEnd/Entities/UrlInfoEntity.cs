using System.ComponentModel.DataAnnotations.Schema;

namespace BackEnd.Models;

public class UrlInfoEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string LongUrl { get; set; }
    public string ShortUrl { get; set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    
    public Guid? UserId { get; set; }
    public virtual UserEntity? User { get; set; }
}
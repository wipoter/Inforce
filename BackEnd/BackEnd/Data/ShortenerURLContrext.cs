using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackEnd.Data;

public class ShortenerUrlContext : DbContext
{
    public ShortenerUrlContext(DbContextOptions<ShortenerUrlContext> options) 
        : base(options)
    {
    }

    public virtual DbSet<UrlInfoEntity> UrlInfo { get; set; }
    public virtual DbSet<LoginInfoEntity> LoginInfo { get; set; }
    public virtual DbSet<UserEntity> User { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Налаштування зв'язку один-до-одного між LoginInfoEntity та UserEntity
        modelBuilder.Entity<UserEntity>()
            .HasOne(u => u.LoginInfo)  // User має один LoginInfo
            .WithOne(l => l.User)      // LoginInfo має одного User
            .HasForeignKey<LoginInfoEntity>(l => l.UserId); // Вказуємо зовнішній ключ

        // Налаштування зв'язку один-до-багатьох між UserEntity та UrlInfoEntity
        modelBuilder.Entity<UrlInfoEntity>()
            .HasOne(u => u.User)  // UrlInfo має одного User
            .WithMany(u => u.UrlInfos)  // User має багато UrlInfo
            .HasForeignKey(u => u.UserId);  // Вказуємо зовнішній ключ

        base.OnModelCreating(modelBuilder);
    }
}

using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Configurations;

public class UrlInfoConfiguration:IEntityTypeConfiguration<UrlInfoEntity>
{
    public void Configure(EntityTypeBuilder<UrlInfoEntity> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.HasOne(u => u.User)  // UrlInfo має одного User
            .WithMany(u => u.UrlInfos)  // User має багато UrlInfo
            .HasForeignKey(u => u.UserId);  // Вказуємо зовнішній ключ
    }
}
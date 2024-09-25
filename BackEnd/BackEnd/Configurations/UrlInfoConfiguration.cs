using BackEnd.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Configurations;

public class UrlInfoConfiguration:IEntityTypeConfiguration<UrlInfoEntity>
{
    public void Configure(EntityTypeBuilder<UrlInfoEntity> builder)
    {
        builder.HasKey(u => u.Id);
        
        builder.HasOne(u => u.User)
            .WithMany(u => u.UrlInfos)
            .HasForeignKey(u => u.UserId);
    }
}
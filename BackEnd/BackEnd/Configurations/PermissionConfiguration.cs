﻿using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Configurations;

public class PermissionConfiguration:IEntityTypeConfiguration<PermissionEntity>
{
    public void Configure(EntityTypeBuilder<PermissionEntity> builder)
    {
        builder.HasKey(p => p.Id);

        var permissions = Enum.GetValues<Permission>()
            .Select(p => new PermissionEntity
            {
                Id = (int)p,
                Name = p.ToString()
            });
        builder.HasData(permissions);
    }
}
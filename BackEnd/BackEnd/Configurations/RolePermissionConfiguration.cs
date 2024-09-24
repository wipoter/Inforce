﻿using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BackEnd.Configurations;

public class RolePermissionConfiguration : IEntityTypeConfiguration<RolePermissionEntity>
{
    private readonly AuthorizationOptions _authorization;

    public RolePermissionConfiguration(AuthorizationOptions authorization)
    {
        _authorization = authorization;
    }

    public void Configure(EntityTypeBuilder<RolePermissionEntity> builder)
    {
        // Налаштування композитного ключа
        builder.HasKey(r => new { r.RoleId, r.PermissionId });

        // Ініціалізація даних
        builder.HasData(ParseRolePermissions());
    }
    
    

    private RolePermissionEntity[] ParseRolePermissions()
    {
        return _authorization.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                }))
            .ToArray();
    }
}
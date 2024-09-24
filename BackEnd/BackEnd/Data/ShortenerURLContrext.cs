using BackEnd.Configurations;
using BackEnd.Interfaces;
using BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackEnd.Data;

public class ShortenerUrlContext(DbContextOptions<ShortenerUrlContext> options,
    IOptions<AuthorizationOptions> authOptions) : DbContext(options)
{

    public virtual DbSet<UrlInfoEntity> UrlInfos { get; set; }
    public virtual DbSet<LoginInfoEntity> LoginInfos { get; set; }
    public virtual DbSet<UserEntity> Users { get; set; }
    public virtual DbSet<RoleEntity> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShortenerUrlContext).Assembly);
        modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));
        
        var rolePermissions = new AuthorizationOptions
        {
            RolePermissions = new[]
            {
                new RolePermissions { Role = "Admin", Permissions = new[] { "Create", "Read", "Update", "Delete" } },
                new RolePermissions { Role = "User", Permissions = new[] { "Create", "Read" } }
            }
        };

        var rolePermissionEntities = rolePermissions.RolePermissions
            .SelectMany(rp => rp.Permissions
                .Select(p => new RolePermissionEntity
                {
                    RoleId = (int)Enum.Parse<Role>(rp.Role),
                    PermissionId = (int)Enum.Parse<Permission>(p)
                }))
            .ToArray();

        modelBuilder.Entity<RolePermissionEntity>().HasData(rolePermissionEntities);
    }
}

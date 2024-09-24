using BackEnd.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace BackEnd.Interfaces;

public class AuthorizationOptions
{
    public RolePermissions[] RolePermissions { get; set; } = [];
}

public class RolePermissions
{
    public string Role { get; set; } = string.Empty;
    public string[] Permissions { get; set; } = [];
}
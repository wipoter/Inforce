using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class role : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginInfo_User_UserId",
                table: "LoginInfo");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlInfo_User_UserId",
                table: "UrlInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrlInfo",
                table: "UrlInfo");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginInfo",
                table: "LoginInfo");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "UrlInfo",
                newName: "UrlInfos");

            migrationBuilder.RenameTable(
                name: "LoginInfo",
                newName: "LoginInfos");

            migrationBuilder.RenameIndex(
                name: "IX_UrlInfo_UserId",
                table: "UrlInfos",
                newName: "IX_UrlInfos_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LoginInfo_UserId",
                table: "LoginInfos",
                newName: "IX_LoginInfos_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrlInfos",
                table: "UrlInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginInfos",
                table: "LoginInfos",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PermissionEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RolePermissionEntity",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    PermissionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolePermissionEntity", x => new { x.RoleId, x.PermissionId });
                    table.ForeignKey(
                        name: "FK_RolePermissionEntity_PermissionEntity_PermissionId",
                        column: x => x.PermissionId,
                        principalTable: "PermissionEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RolePermissionEntity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoleEntity",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoleEntity", x => new { x.RoleId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserRoleEntity_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoleEntity_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "PermissionEntity",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Read" },
                    { 2, "Create" },
                    { 3, "Update" },
                    { 4, "Delete" }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Admin" },
                    { 2, "User" }
                });

            migrationBuilder.InsertData(
                table: "RolePermissionEntity",
                columns: new[] { "PermissionId", "RoleId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 1 },
                    { 4, 1 },
                    { 1, 2 },
                    { 2, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RolePermissionEntity_PermissionId",
                table: "RolePermissionEntity",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoleEntity_UserId",
                table: "UserRoleEntity",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginInfos_Users_UserId",
                table: "LoginInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlInfos_Users_UserId",
                table: "UrlInfos",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LoginInfos_Users_UserId",
                table: "LoginInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UrlInfos_Users_UserId",
                table: "UrlInfos");

            migrationBuilder.DropTable(
                name: "RolePermissionEntity");

            migrationBuilder.DropTable(
                name: "UserRoleEntity");

            migrationBuilder.DropTable(
                name: "PermissionEntity");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrlInfos",
                table: "UrlInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LoginInfos",
                table: "LoginInfos");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "UrlInfos",
                newName: "UrlInfo");

            migrationBuilder.RenameTable(
                name: "LoginInfos",
                newName: "LoginInfo");

            migrationBuilder.RenameIndex(
                name: "IX_UrlInfos_UserId",
                table: "UrlInfo",
                newName: "IX_UrlInfo_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_LoginInfos_UserId",
                table: "LoginInfo",
                newName: "IX_LoginInfo_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrlInfo",
                table: "UrlInfo",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LoginInfo",
                table: "LoginInfo",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LoginInfo_User_UserId",
                table: "LoginInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlInfo_User_UserId",
                table: "UrlInfo",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}

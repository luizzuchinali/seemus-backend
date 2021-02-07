using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seemus.Infra.Migrations
{
    public partial class AddRefreshTokenToUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "Users",
                type: "varchar(100)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiration",
                table: "Users",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7fe87d14-1645-4f82-9c97-fa602b9ad9f6"),
                column: "ConcurrencyStamp",
                value: "29242408-3bac-4631-b9b0-ec87d0572739");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiration",
                table: "Users");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7fe87d14-1645-4f82-9c97-fa602b9ad9f6"),
                column: "ConcurrencyStamp",
                value: "be07108b-29f0-4879-b89a-681bc82ca2fc");
        }
    }
}

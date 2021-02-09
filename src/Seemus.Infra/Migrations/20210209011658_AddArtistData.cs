using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Seemus.Infra.Migrations
{
    public partial class AddArtistData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileImageUrl = table.Column<string>(type: "text", nullable: true),
                    Online = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Artists_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7fe87d14-1645-4f82-9c97-fa602b9ad9f6"),
                column: "ConcurrencyStamp",
                value: "644e4eb4-5342-4a01-9d76-2eabf8b9337c");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("7fe87d14-1645-4f82-9c97-fa602b9ad9f6"),
                column: "ConcurrencyStamp",
                value: "29242408-3bac-4631-b9b0-ec87d0572739");
        }
    }
}

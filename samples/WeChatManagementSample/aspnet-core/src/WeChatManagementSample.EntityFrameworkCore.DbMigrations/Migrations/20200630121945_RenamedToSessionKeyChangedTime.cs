using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class RenamedToSessionKeyChangedTime : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionKeyModificationTime",
                table: "MiniProgramsMiniProgramUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionKeyChangedTime",
                table: "MiniProgramsMiniProgramUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionKeyChangedTime",
                table: "MiniProgramsMiniProgramUsers");

            migrationBuilder.AddColumn<DateTime>(
                name: "SessionKeyModificationTime",
                table: "MiniProgramsMiniProgramUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}

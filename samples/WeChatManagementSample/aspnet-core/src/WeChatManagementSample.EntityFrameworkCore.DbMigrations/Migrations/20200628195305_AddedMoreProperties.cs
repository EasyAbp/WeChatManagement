using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class AddedMoreProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UnionId",
                table: "MiniProgramsMiniProgramUsers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OpenAppId",
                table: "MiniProgramsMiniPrograms",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "WeChatComponentId",
                table: "MiniProgramsMiniPrograms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UnionId",
                table: "MiniProgramsMiniProgramUsers");

            migrationBuilder.DropColumn(
                name: "OpenAppId",
                table: "MiniProgramsMiniPrograms");

            migrationBuilder.DropColumn(
                name: "WeChatComponentId",
                table: "MiniProgramsMiniPrograms");
        }
    }
}

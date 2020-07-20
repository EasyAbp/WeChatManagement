using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class ChangedPrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniProgramsUserInfos",
                table: "MiniProgramsUserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniProgramsMiniProgramUsers",
                table: "MiniProgramsMiniProgramUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MiniProgramsMiniPrograms",
                table: "MiniProgramsMiniPrograms");

            migrationBuilder.RenameTable(
                name: "MiniProgramsUserInfos",
                newName: "WeChatManagementMiniProgramsUserInfos");

            migrationBuilder.RenameTable(
                name: "MiniProgramsMiniProgramUsers",
                newName: "WeChatManagementMiniProgramsMiniProgramUsers");

            migrationBuilder.RenameTable(
                name: "MiniProgramsMiniPrograms",
                newName: "WeChatManagementMiniProgramsMiniPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_MiniProgramsMiniPrograms_Name",
                table: "WeChatManagementMiniProgramsMiniPrograms",
                newName: "IX_WeChatManagementMiniProgramsMiniPrograms_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeChatManagementMiniProgramsUserInfos",
                table: "WeChatManagementMiniProgramsUserInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeChatManagementMiniProgramsMiniProgramUsers",
                table: "WeChatManagementMiniProgramsMiniProgramUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_WeChatManagementMiniProgramsMiniPrograms",
                table: "WeChatManagementMiniProgramsMiniPrograms",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_WeChatManagementMiniProgramsUserInfos",
                table: "WeChatManagementMiniProgramsUserInfos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeChatManagementMiniProgramsMiniProgramUsers",
                table: "WeChatManagementMiniProgramsMiniProgramUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_WeChatManagementMiniProgramsMiniPrograms",
                table: "WeChatManagementMiniProgramsMiniPrograms");

            migrationBuilder.RenameTable(
                name: "WeChatManagementMiniProgramsUserInfos",
                newName: "MiniProgramsUserInfos");

            migrationBuilder.RenameTable(
                name: "WeChatManagementMiniProgramsMiniProgramUsers",
                newName: "MiniProgramsMiniProgramUsers");

            migrationBuilder.RenameTable(
                name: "WeChatManagementMiniProgramsMiniPrograms",
                newName: "MiniProgramsMiniPrograms");

            migrationBuilder.RenameIndex(
                name: "IX_WeChatManagementMiniProgramsMiniPrograms_Name",
                table: "MiniProgramsMiniPrograms",
                newName: "IX_MiniProgramsMiniPrograms_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniProgramsUserInfos",
                table: "MiniProgramsUserInfos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniProgramsMiniProgramUsers",
                table: "MiniProgramsMiniProgramUsers",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MiniProgramsMiniPrograms",
                table: "MiniProgramsMiniPrograms",
                column: "Id");
        }
    }
}

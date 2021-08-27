using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class IntroducedWeChatApp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpWeChatManagementMiniProgramsMiniProgramUsers",
                table: "EasyAbpWeChatManagementMiniProgramsMiniProgramUsers");
            
            migrationBuilder.RenameTable(
                name: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                newName: "EasyAbpWeChatManagementCommonWeChatApps");

            migrationBuilder.RenameTable(
                name: "EasyAbpWeChatManagementMiniProgramsMiniProgramUsers",
                newName: "EasyAbpWeChatManagementCommonWeChatAppUsers");
            
            migrationBuilder.RenameColumn(
                name: "MiniProgramId",
                table: "EasyAbpWeChatManagementCommonWeChatAppUsers",
                newName: "WeChatAppId");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpWeChatManagementCommonWeChatApps",
                table: "EasyAbpWeChatManagementCommonWeChatApps",
                column: "Id");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpWeChatManagementCommonWeChatAppUsers",
                table: "EasyAbpWeChatManagementCommonWeChatAppUsers",
                column: "Id");
            
            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "EasyAbpWeChatManagementCommonWeChatApps",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpWeChatManagementCommonWeChatApps",
                table: "EasyAbpWeChatManagementCommonWeChatApps");
            
            migrationBuilder.DropPrimaryKey(
                name: "PK_EasyAbpWeChatManagementCommonWeChatAppUsers",
                table: "EasyAbpWeChatManagementCommonWeChatAppUsers");
            
            migrationBuilder.RenameTable(
                name: "EasyAbpWeChatManagementCommonWeChatApps",
                newName: "EasyAbpWeChatManagementMiniProgramsMiniPrograms");

            migrationBuilder.RenameTable(
                name: "EasyAbpWeChatManagementCommonWeChatAppUsers",
                newName: "EasyAbpWeChatManagementMiniProgramsMiniProgramUsers");
            
            migrationBuilder.RenameColumn(
                name: "WeChatAppId",
                table: "EasyAbpWeChatManagementMiniProgramsMiniProgramUsers",
                newName: "MiniProgramId");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                column: "Id");
            
            migrationBuilder.AddPrimaryKey(
                name: "PK_EasyAbpWeChatManagementMiniProgramsMiniProgramUsers",
                table: "EasyAbpWeChatManagementMiniProgramsMiniProgramUsers",
                column: "Id");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeChatManagementSample.Migrations
{
    public partial class RenamedToComponentWeChatAppId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "WeChatComponentId",
                table: "EasyAbpWeChatManagementCommonWeChatApps",
                newName: "ComponentWeChatAppId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ComponentWeChatAppId",
                table: "EasyAbpWeChatManagementCommonWeChatApps",
                newName: "WeChatComponentId");
        }
    }
}

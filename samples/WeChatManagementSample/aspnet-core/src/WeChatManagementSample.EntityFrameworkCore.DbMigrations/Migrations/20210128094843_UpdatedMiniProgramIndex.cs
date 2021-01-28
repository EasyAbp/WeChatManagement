using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class UpdatedMiniProgramIndex : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EasyAbpWeChatManagementMiniProgramsMiniPrograms_Name",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpWeChatManagementMiniProgramsMiniPrograms_Name_TenantId",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                columns: new[] { "Name", "TenantId" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [TenantId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EasyAbpWeChatManagementMiniProgramsMiniPrograms_Name_TenantId",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms");

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpWeChatManagementMiniProgramsMiniPrograms_Name",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }
    }
}

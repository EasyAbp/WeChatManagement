using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class RemovedNameUniqueIndexInMiniProgram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_EasyAbpWeChatManagementMiniProgramsMiniPrograms_Name_TenantId",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpWeChatManagementMiniProgramsMiniPrograms_Name_TenantId",
                table: "EasyAbpWeChatManagementMiniProgramsMiniPrograms",
                columns: new[] { "Name", "TenantId" },
                unique: true,
                filter: "[Name] IS NOT NULL AND [TenantId] IS NOT NULL");
        }
    }
}

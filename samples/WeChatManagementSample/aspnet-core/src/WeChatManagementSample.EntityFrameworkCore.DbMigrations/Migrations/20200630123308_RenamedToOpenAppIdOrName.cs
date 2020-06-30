using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class RenamedToOpenAppIdOrName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenAppId",
                table: "MiniProgramsMiniPrograms");

            migrationBuilder.AddColumn<string>(
                name: "OpenAppIdOrName",
                table: "MiniProgramsMiniPrograms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OpenAppIdOrName",
                table: "MiniProgramsMiniPrograms");

            migrationBuilder.AddColumn<string>(
                name: "OpenAppId",
                table: "MiniProgramsMiniPrograms",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

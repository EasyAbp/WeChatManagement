using Microsoft.EntityFrameworkCore.Migrations;

namespace WeChatManagementSample.Migrations
{
    public partial class MadeMiniProgramNameUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MiniProgramsMiniPrograms",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_MiniProgramsMiniPrograms_Name",
                table: "MiniProgramsMiniPrograms",
                column: "Name",
                unique: true,
                filter: "[Name] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_MiniProgramsMiniPrograms_Name",
                table: "MiniProgramsMiniPrograms");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "MiniProgramsMiniPrograms",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}

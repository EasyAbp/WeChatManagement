using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeChatManagementSample.Migrations
{
    public partial class RemovedEncryptedAccessTokenProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EncryptedAccessToken",
                table: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EncryptedAccessToken",
                table: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

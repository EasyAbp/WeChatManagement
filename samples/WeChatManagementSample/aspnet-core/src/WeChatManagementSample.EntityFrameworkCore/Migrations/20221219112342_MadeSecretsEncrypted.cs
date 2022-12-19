using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeChatManagementSample.Migrations
{
    public partial class MadeSecretsEncrypted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SessionKey",
                table: "EasyAbpWeChatManagementCommonWeChatAppUsers",
                newName: "EncryptedSessionKey");

            migrationBuilder.RenameColumn(
                name: "AppSecret",
                table: "EasyAbpWeChatManagementCommonWeChatApps",
                newName: "EncryptedAppSecret");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EncryptedSessionKey",
                table: "EasyAbpWeChatManagementCommonWeChatAppUsers",
                newName: "SessionKey");

            migrationBuilder.RenameColumn(
                name: "EncryptedAppSecret",
                table: "EasyAbpWeChatManagementCommonWeChatApps",
                newName: "AppSecret");
        }
    }
}

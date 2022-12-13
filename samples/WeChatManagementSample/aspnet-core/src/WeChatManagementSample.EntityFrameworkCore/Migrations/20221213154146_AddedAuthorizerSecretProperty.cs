using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeChatManagementSample.Migrations
{
    public partial class AddedAuthorizerSecretProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ComponentAppId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    AuthorizerAppId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    EncryptedAccessToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EncryptedRefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExtraProperties = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: true),
                    CreationTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatorId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LastModificationTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    LastModifierId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DeleterId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletionTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets_ComponentAppId_AuthorizerAppId",
                table: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets",
                columns: new[] { "ComponentAppId", "AuthorizerAppId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets");
        }
    }
}

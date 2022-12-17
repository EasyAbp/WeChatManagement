using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeChatManagementSample.Migrations
{
    public partial class AddedCategoryIdsProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CategoryIds",
                table: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CategoryIds",
                table: "EasyAbpWeChatManagementThirdPartyPlatformsAuthorizerSecrets");
        }
    }
}

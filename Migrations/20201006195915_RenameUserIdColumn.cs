using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class RenameUserIdColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TaskUserUpdates");

            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "TaskUserUpdates",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "TaskUserUpdates");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TaskUserUpdates",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class FifthMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "TaskUserUpdates");

            migrationBuilder.DropColumn(
                name: "UpdatedByUserId",
                table: "TaskUpdates");

            migrationBuilder.AddColumn<int>(
                name: "UpdaterId",
                table: "TaskUserUpdates",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdaterId",
                table: "TaskUpdates",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "TaskUserUpdates");

            migrationBuilder.DropColumn(
                name: "UpdaterId",
                table: "TaskUpdates");

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "TaskUserUpdates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedByUserId",
                table: "TaskUpdates",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}

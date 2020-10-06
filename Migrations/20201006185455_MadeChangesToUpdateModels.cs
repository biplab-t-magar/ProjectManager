using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class MadeChangesToUpdateModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeStamp",
                table: "TaskUserUpdates");

            migrationBuilder.DropColumn(
                name: "TimeAdded",
                table: "TaskUsers");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskUpdates");

            migrationBuilder.AlterColumn<string>(
                name: "UpdaterId",
                table: "TaskUserUpdates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAdded",
                table: "TaskUserUpdates",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UpdaterId",
                table: "TaskUpdates",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "TaskStatus",
                table: "TaskUpdates",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeAdded",
                table: "TaskUserUpdates");

            migrationBuilder.DropColumn(
                name: "TaskStatus",
                table: "TaskUpdates");

            migrationBuilder.AlterColumn<int>(
                name: "UpdaterId",
                table: "TaskUserUpdates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeStamp",
                table: "TaskUserUpdates",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "TimeAdded",
                table: "TaskUsers",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "UpdaterId",
                table: "TaskUpdates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TaskUpdates",
                type: "text",
                nullable: true);
        }
    }
}

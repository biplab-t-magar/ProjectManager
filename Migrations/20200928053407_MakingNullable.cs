using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class MakingNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskUpdates_TaskTypes_TaskTypeId",
                table: "TaskUpdates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeRemoved",
                table: "TaskUserUpdates",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<int>(
                name: "TaskTypeId",
                table: "TaskUpdates",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "TaskUpdates",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskUpdates_TaskTypes_TaskTypeId",
                table: "TaskUpdates",
                column: "TaskTypeId",
                principalTable: "TaskTypes",
                principalColumn: "TaskTypeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskUpdates_TaskTypes_TaskTypeId",
                table: "TaskUpdates");

            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeRemoved",
                table: "TaskUserUpdates",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TaskTypeId",
                table: "TaskUpdates",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "Deadline",
                table: "TaskUpdates",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TaskUpdates_TaskTypes_TaskTypeId",
                table: "TaskUpdates",
                column: "TaskTypeId",
                principalTable: "TaskTypes",
                principalColumn: "TaskTypeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

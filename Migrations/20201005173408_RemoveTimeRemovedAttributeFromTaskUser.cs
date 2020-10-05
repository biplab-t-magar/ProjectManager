using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class RemoveTimeRemovedAttributeFromTaskUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimeRemoved",
                table: "TaskUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "TimeRemoved",
                table: "TaskUsers",
                type: "timestamp without time zone",
                nullable: true);
        }
    }
}

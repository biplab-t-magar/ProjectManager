/*
Generated automatically by Entity Framework Core to build and update the database scheme
*/
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProjectManager.Migrations
{
    public partial class RenamedTimeCreatedInTaskComment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskComments_Projects_ProjectId",
                table: "TaskComments");

            migrationBuilder.DropIndex(
                name: "IX_TaskComments_ProjectId",
                table: "TaskComments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "TaskComments");

            migrationBuilder.RenameColumn(
                name: "timeAdded",
                table: "TaskComments",
                newName: "TimeAdded");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimeAdded",
                table: "TaskComments",
                newName: "timeAdded");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "TaskComments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TaskComments_ProjectId",
                table: "TaskComments",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskComments_Projects_ProjectId",
                table: "TaskComments",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

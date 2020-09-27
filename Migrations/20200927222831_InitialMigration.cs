using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ProjectManager.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: false),
                    TimeCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.ProjectId);
                });

            migrationBuilder.CreateTable(
                name: "UserUpdates",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    MiddleName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Bio = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserUpdates", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypes",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DefaultUrgency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypes", x => new { x.ProjectId, x.TaskTypeId });
                    table.ForeignKey(
                        name: "FK_TaskTypes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUpdates",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    ProjectUpdateId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUpdates", x => new { x.ProjectId, x.ProjectUpdateId });
                    table.ForeignKey(
                        name: "FK_ProjectUpdates_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUpdates_UserUpdates_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUsers",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    TimeAdded = table.Column<DateTime>(nullable: false),
                    TimeRemoved = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUsers", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectUsers_UserUpdates_UserId",
                        column: x => x.UserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectUserUpdates",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    ProjectUserUpdateId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    TimeRemoved = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectUserUpdates", x => new { x.ProjectId, x.UserId, x.ProjectUserUpdateId });
                    table.ForeignKey(
                        name: "FK_ProjectUserUpdates_UserUpdates_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    TaskStatus = table.Column<string>(nullable: false),
                    Urgency = table.Column<string>(nullable: false),
                    TimeCreate = table.Column<DateTime>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false),
                    TaskTypeProjectId = table.Column<int>(nullable: false),
                    TaskTypeId1 = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => new { x.ProjectId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_Tasks_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_TaskTypes_TaskTypeProjectId_TaskTypeId1",
                        columns: x => new { x.TaskTypeProjectId, x.TaskTypeId1 },
                        principalTable: "TaskTypes",
                        principalColumns: new[] { "ProjectId", "TaskTypeId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskTypeUpdates",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    TaskTypeId = table.Column<int>(nullable: false),
                    TaskTypeUpdateId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    DefaultUrgency = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskTypeUpdates", x => new { x.ProjectId, x.TaskTypeId, x.TaskTypeUpdateId });
                    table.ForeignKey(
                        name: "FK_TaskTypeUpdates_UserUpdates_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskTypeUpdates_TaskTypes_ProjectId_TaskTypeId",
                        columns: x => new { x.ProjectId, x.TaskTypeId },
                        principalTable: "TaskTypes",
                        principalColumns: new[] { "ProjectId", "TaskTypeId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskUpdates",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    TaskProjectId = table.Column<int>(nullable: false),
                    TaskId1 = table.Column<int>(nullable: false),
                    TaskUpdateId = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    TaskStatus = table.Column<string>(nullable: true),
                    Urgency = table.Column<string>(nullable: true),
                    TaskTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUpdates", x => new { x.ProjectId, x.TaskId });
                    table.ForeignKey(
                        name: "FK_TaskUpdates_UserUpdates_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUpdates_Tasks_TaskProjectId_TaskId1",
                        columns: x => new { x.TaskProjectId, x.TaskId1 },
                        principalTable: "Tasks",
                        principalColumns: new[] { "ProjectId", "TaskId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskUsers",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TimeAdded = table.Column<DateTime>(nullable: false),
                    TimeRemoved = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUsers", x => new { x.ProjectId, x.TaskId, x.UserId });
                    table.ForeignKey(
                        name: "FK_TaskUsers_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUsers_UserUpdates_UserId",
                        column: x => x.UserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUsers_Tasks_ProjectId_TaskId",
                        columns: x => new { x.ProjectId, x.TaskId },
                        principalTable: "Tasks",
                        principalColumns: new[] { "ProjectId", "TaskId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskUserUpdates",
                columns: table => new
                {
                    ProjectId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    TaskUserUpdateId = table.Column<int>(nullable: false),
                    UpdatedByUserId = table.Column<int>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    TimeRemoved = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskUserUpdates", x => new { x.ProjectId, x.TaskId, x.UserId, x.TaskUserUpdateId });
                    table.ForeignKey(
                        name: "FK_TaskUserUpdates_UserUpdates_UpdatedByUserId",
                        column: x => x.UpdatedByUserId,
                        principalTable: "UserUpdates",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskUserUpdates_TaskUsers_ProjectId_TaskId_UserId",
                        columns: x => new { x.ProjectId, x.TaskId, x.UserId },
                        principalTable: "TaskUsers",
                        principalColumns: new[] { "ProjectId", "TaskId", "UserId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUpdates_UpdatedByUserId",
                table: "ProjectUpdates",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUsers_UserId",
                table: "ProjectUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectUserUpdates_UpdatedByUserId",
                table: "ProjectUserUpdates",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_TaskTypeProjectId_TaskTypeId1",
                table: "Tasks",
                columns: new[] { "TaskTypeProjectId", "TaskTypeId1" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskTypeUpdates_UpdatedByUserId",
                table: "TaskTypeUpdates",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskUpdates_UpdatedByUserId",
                table: "TaskUpdates",
                column: "UpdatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskUpdates_TaskProjectId_TaskId1",
                table: "TaskUpdates",
                columns: new[] { "TaskProjectId", "TaskId1" });

            migrationBuilder.CreateIndex(
                name: "IX_TaskUsers_UserId",
                table: "TaskUsers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskUserUpdates_UpdatedByUserId",
                table: "TaskUserUpdates",
                column: "UpdatedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectUpdates");

            migrationBuilder.DropTable(
                name: "ProjectUsers");

            migrationBuilder.DropTable(
                name: "ProjectUserUpdates");

            migrationBuilder.DropTable(
                name: "TaskTypeUpdates");

            migrationBuilder.DropTable(
                name: "TaskUpdates");

            migrationBuilder.DropTable(
                name: "TaskUserUpdates");

            migrationBuilder.DropTable(
                name: "TaskUsers");

            migrationBuilder.DropTable(
                name: "UserUpdates");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "TaskTypes");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}

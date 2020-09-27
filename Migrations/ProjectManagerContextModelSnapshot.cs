﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectManager.Data;

namespace ProjectManager.Migrations
{
    [DbContext(typeof(ProjectManagerContext))]
    partial class ProjectManagerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("ProjectManager.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUpdate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectUpdateId")
                        .HasColumnType("integer");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId", "ProjectUpdateId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("ProjectUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUser", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUserUpdate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("ProjectUserUpdateId")
                        .HasColumnType("integer");

                    b.Property<string>("Role")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId", "UserId", "ProjectUserUpdateId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("ProjectUserUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.Task", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("TaskStatus")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("TaskTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskTypeId1")
                        .HasColumnType("integer");

                    b.Property<int>("TaskTypeProjectId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeCreate")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Urgency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProjectId", "TaskId");

                    b.HasIndex("TaskTypeProjectId", "TaskTypeId1");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskType", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskTypeId")
                        .HasColumnType("integer");

                    b.Property<string>("DefaultUrgency")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProjectId", "TaskTypeId");

                    b.ToTable("TaskTypes");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskTypeUpdate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskTypeUpdateId")
                        .HasColumnType("integer");

                    b.Property<string>("DefaultUrgency")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId", "TaskTypeId", "TaskTypeUpdateId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("TaskTypeUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUpdate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int>("TaskId1")
                        .HasColumnType("integer");

                    b.Property<int>("TaskProjectId")
                        .HasColumnType("integer");

                    b.Property<string>("TaskStatus")
                        .HasColumnType("text");

                    b.Property<int>("TaskTypeId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskUpdateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Urgency")
                        .HasColumnType("text");

                    b.HasKey("ProjectId", "TaskId");

                    b.HasIndex("UpdatedByUserId");

                    b.HasIndex("TaskProjectId", "TaskId1");

                    b.ToTable("TaskUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUser", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeAdded")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ProjectId", "TaskId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("TaskUsers");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUserUpdate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskUserUpdateId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId", "TaskId", "UserId", "TaskUserUpdateId");

                    b.HasIndex("UpdatedByUserId");

                    b.ToTable("TaskUserUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("UserUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUpdate", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany("ProjectUpdates")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUser", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.User", "User")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUserUpdate", b =>
                {
                    b.HasOne("ProjectManager.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.Task", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.TaskType", "TaskType")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskTypeProjectId", "TaskTypeId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskType", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskTypeUpdate", b =>
                {
                    b.HasOne("ProjectManager.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.TaskType", "TaskType")
                        .WithMany("TaskTypeUpdates")
                        .HasForeignKey("ProjectId", "TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUpdate", b =>
                {
                    b.HasOne("ProjectManager.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.Task", "Task")
                        .WithMany("TaskUpdates")
                        .HasForeignKey("TaskProjectId", "TaskId1")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUser", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany()
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.User", "User")
                        .WithMany("TaskUsers")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.Task", "Task")
                        .WithMany("TaskUsers")
                        .HasForeignKey("ProjectId", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUserUpdate", b =>
                {
                    b.HasOne("ProjectManager.Models.User", "UpdatedBy")
                        .WithMany()
                        .HasForeignKey("UpdatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.TaskUser", "TaskUser")
                        .WithMany()
                        .HasForeignKey("ProjectId", "TaskId", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

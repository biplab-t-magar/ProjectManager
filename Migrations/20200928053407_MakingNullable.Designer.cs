﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ProjectManager.Data;

namespace ProjectManager.Migrations
{
    [DbContext(typeof(ProjectManagerContext))]
    [Migration("20200928053407_MakingNullable")]
    partial class MakingNullable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .HasColumnType("character varying(700)")
                        .HasMaxLength(700);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(250)")
                        .HasMaxLength(250);

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp without time zone");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
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

                    b.HasKey("ProjectId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("ProjectUsers");
                });

            modelBuilder.Entity("ProjectManager.Models.Task", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("character varying(500)")
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TaskStatus")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Open");

                    b.Property<int>("TaskTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeCreated")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Urgency")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("ProjectId", "TaskId");

                    b.HasIndex("TaskTypeId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskType", b =>
                {
                    b.Property<int>("TaskTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("DefaultUrgency")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("text")
                        .HasDefaultValue("Medium");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.HasKey("TaskTypeId");

                    b.HasIndex("ProjectId");

                    b.ToTable("TaskTypes");
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUpdate", b =>
                {
                    b.Property<int>("ProjectId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskId")
                        .HasColumnType("integer");

                    b.Property<int>("TaskUpdateId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("Deadline")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Name")
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("Status")
                        .HasColumnType("text");

                    b.Property<int?>("TaskTypeId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.Property<string>("Urgency")
                        .HasColumnType("text");

                    b.HasKey("ProjectId", "TaskId", "TaskUpdateId");

                    b.HasIndex("TaskTypeId");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime?>("TimeRemoved")
                        .HasColumnType("timestamp without time zone");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("UpdatedByUserId")
                        .HasColumnType("integer");

                    b.HasKey("ProjectId", "TaskId", "UserId", "TaskUserUpdateId");

                    b.ToTable("TaskUserUpdates");
                });

            modelBuilder.Entity("ProjectManager.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("Bio")
                        .HasColumnType("character varying(300)")
                        .HasMaxLength(300);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("character varying(50)")
                        .HasMaxLength(50);

                    b.Property<string>("MiddleName")
                        .HasColumnType("text");

                    b.HasKey("UserId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ProjectManager.Models.ProjectUser", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany("ProjectUsers")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.Task", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", null)
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ProjectManager.Models.TaskType", "TaskType")
                        .WithMany("Tasks")
                        .HasForeignKey("TaskTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskType", b =>
                {
                    b.HasOne("ProjectManager.Models.Project", "Project")
                        .WithMany("TaskTypes")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUpdate", b =>
                {
                    b.HasOne("ProjectManager.Models.TaskType", "TaskType")
                        .WithMany()
                        .HasForeignKey("TaskTypeId");

                    b.HasOne("ProjectManager.Models.Task", "Task")
                        .WithMany()
                        .HasForeignKey("ProjectId", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ProjectManager.Models.TaskUser", b =>
                {
                    b.HasOne("ProjectManager.Models.User", "User")
                        .WithMany()
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
                    b.HasOne("ProjectManager.Models.TaskUser", "TaskUser")
                        .WithMany("TaskUserUpdates")
                        .HasForeignKey("ProjectId", "TaskId", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}

using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    public class ProjectManagerContext : DbContext
    {
        public ProjectManagerContext (DbContextOptions<ProjectManagerContext> opt) : base(opt)
        { 

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //assign key to table
            modelBuilder.Entity<Project>()
                .HasKey(p => p.ProjectId);
            
            //auto-increment id attribute
            
            modelBuilder.Entity<Project>()
                .Property(p => p.ProjectId)
                .ValueGeneratedOnAdd();
            
            //assign key to ProjectUser table
                
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pu => new {pu.ProjectId, pu.UserId});

            modelBuilder.Entity<User>()
                .ToTable("Users")
                .HasKey(u => u.UserId);

            modelBuilder.Entity<User>()
                .Property(u => u.UserId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<Task>()
                .HasKey(t => new {t.ProjectId, t.TaskId});

            modelBuilder.Entity<Task>()
                .Property(t => t.TaskStatus)
                .HasDefaultValue("Open");
            
            modelBuilder.Entity<Task>()
                .Property(t => t.TaskId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskUpdate>()
                .HasKey(tu => new {tu.ProjectId, tu.TaskId, tu.TaskUpdateId});  

            modelBuilder.Entity<TaskUpdate>()
                .Property(tu => tu.TaskUpdateId)
                .ValueGeneratedOnAdd();  

            modelBuilder.Entity<TaskType>()
                .HasKey(tt => new {tt.TaskTypeId});

            modelBuilder.Entity<TaskType>()
                .Property(tt => tt.TaskTypeId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskType>()
                .Property(tt => tt.DefaultUrgency)
                .HasDefaultValue("Medium");

            modelBuilder.Entity<TaskUser>()
                .HasKey(tus => new {tus.ProjectId, tus.TaskId, tus.UserId});


            modelBuilder.Entity<TaskUserUpdate>()
                .HasKey(tuu => new {tuu.ProjectId, tuu.TaskId, tuu.UserId, tuu.TaskUserUpdateId});
            
            modelBuilder.Entity<TaskUserUpdate>()
                .Property(tuu => tuu.TaskUserUpdateId)
                .ValueGeneratedOnAdd();
        } 

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<TaskUpdate> TaskUpdates {get; set;}
        public DbSet<TaskType> TaskTypes {get; set;}
        public DbSet<TaskUser> TaskUsers {get; set;}
        public DbSet<TaskUserUpdate> TaskUserUpdates {get; set;}
        public DbSet<User> Users {get; set;}

    }
}
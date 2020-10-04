using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjectManager.Models;

namespace ProjectManager.Data
{
    //Database context that accesses all the database tables in the application 
    public class ProjectManagerContext : IdentityDbContext<AppUser>
    {
        public ProjectManagerContext (DbContextOptions<ProjectManagerContext> options) : base(options)
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
                .HasKey(pu => new {pu.ProjectId, pu.AppUserId});

            // modelBuilder.Entity<User>()
            //     .ToTable("Users")
            //     .HasKey(u => u.UserId);

            // modelBuilder.Entity<User>()
            //     .Property(u => u.UserId)
            //     .ValueGeneratedOnAdd();

            modelBuilder.Entity<Task>()
                .HasKey(t => new {t.TaskId});

            modelBuilder.Entity<Task>()
                .Property(t => t.TaskStatus)
                .HasDefaultValue("Open");
            
            modelBuilder.Entity<Task>()
                .Property(t => t.TaskId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskUpdate>()
                .HasKey(tu => new {tu.TaskUpdateId});  
                

            modelBuilder.Entity<TaskUpdate>()
                .Property(tu => tu.TaskUpdateId)
                .ValueGeneratedOnAdd();

                

            modelBuilder.Entity<TaskType>()
                .HasKey(tt => new {tt.TaskTypeId});

            modelBuilder.Entity<TaskType>()
                .Property(tt => tt.TaskTypeId)
                .ValueGeneratedOnAdd();


            modelBuilder.Entity<TaskUser>()
                .HasKey(tus => new {tus.TaskId, tus.AppUserId});


            modelBuilder.Entity<TaskUserUpdate>()
                .HasKey(tuu => new {tuu.TaskUserUpdateId});
            
            modelBuilder.Entity<TaskUserUpdate>()
                .Property(tuu => tuu.TaskUserUpdateId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<ProjectInvitation>()
                .HasKey(pu => pu.ProjectInvitationId);

            modelBuilder.Entity<ProjectInvitation>()
                .Property(pu => pu.ProjectInvitationId)
                .ValueGeneratedOnAdd();

            modelBuilder.Entity<TaskComment>()
                .HasKey(pu => pu.TaskCommentId);

            modelBuilder.Entity<TaskComment>()
                .Property(pu => pu.TaskCommentId)
                .ValueGeneratedOnAdd();

            //change name of AppUser's primary key from Id to AppUserId


            base.OnModelCreating(modelBuilder);
        } 

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUser> ProjectUsers {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<TaskUpdate> TaskUpdates {get; set;}
        public DbSet<TaskType> TaskTypes {get; set;}
        public DbSet<TaskUser> TaskUsers {get; set;}
        public DbSet<TaskUserUpdate> TaskUserUpdates {get; set;}
        public DbSet<AppUser> AppUsers {get; set;}
        public DbSet<ProjectInvitation> ProjectInvitations {get; set;}
        public DbSet<TaskComment> TaskComments {get; set;}

    }
}
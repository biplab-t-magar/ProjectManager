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
            modelBuilder.Entity<Project>()
                .HasKey(p => p.ProjectId);
                

            modelBuilder.Entity<ProjectUpdate>()
                .HasKey(pu => new {pu.ProjectId, pu.ProjectUpdateId});
            
            modelBuilder.Entity<ProjectUser>()
                .HasKey(pus => new {pus.ProjectId, pus.UserId});
            
            modelBuilder.Entity<ProjectUserUpdate>()
                .HasKey(puu => new {puu.ProjectId, puu.UserId, puu.ProjectUserUpdateId});
            

            modelBuilder.Entity<Task>()
                .HasKey(t => new {t.ProjectId, t.TaskId});

            modelBuilder.Entity<TaskUpdate>()
                .HasKey(tu => new {tu.ProjectId, tu.TaskId});    

            modelBuilder.Entity<TaskType>()
                .HasKey(tt => new {tt.ProjectId, tt.TaskTypeId});
            
            modelBuilder.Entity<TaskTypeUpdate>()
                .HasKey(ttu => new {ttu.ProjectId, ttu.TaskTypeId, ttu.TaskTypeUpdateId});

            modelBuilder.Entity<TaskUser>()
                .HasKey(tus => new {tus.ProjectId, tus.TaskId, tus.UserId});

            modelBuilder.Entity<TaskUserUpdate>()
                .HasKey(tuu => new {tuu.ProjectId, tuu.TaskId, tuu.UserId, tuu.TaskUserUpdateId});
            
            modelBuilder.Entity<User>()
                .HasKey(u => u.UserId);
                
        } 

        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectUpdate> ProjectUpdates {get; set;}
        public DbSet<ProjectUser> ProjectUsers {get; set;}
        public DbSet<ProjectUserUpdate> ProjectUserUpdates {get; set;}
        public DbSet<Task> Tasks {get; set;}
        public DbSet<TaskUpdate> TaskUpdates {get; set;}
        public DbSet<TaskType> TaskTypes {get; set;}
        public DbSet<TaskTypeUpdate> TaskTypeUpdates {get; set;}
        public DbSet<TaskUser> TaskUsers {get; set;}
        public DbSet<TaskUserUpdate> TaskUserUpdates {get; set;}
        public DbSet<User> UserUpdates {get; set;}

    }
}
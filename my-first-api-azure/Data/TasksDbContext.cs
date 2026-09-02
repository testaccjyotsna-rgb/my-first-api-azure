using Microsoft.EntityFrameworkCore;
using my_first_api_azure.Models;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace my_first_api_azure.Data
{
    public class TasksDbContext : DbContext
    {
        public TasksDbContext(DbContextOptions<TasksDbContext> options) : base(options) { }

        public DbSet<TaskModel> Tasks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TaskModel>(entity =>
            {
                entity.HasKey(t => t.TaskId);
                entity.Property(t => t.TaskName).IsRequired();
                entity.Property(t => t.TaskCreatedBy).IsRequired();
                entity.Property(t => t.TaskAssignedTo).IsRequired();

                // IsOverDue is computed at query time, not stored in the DB
                entity.Ignore(t => t.IsOverDue);
            });
        }
    }
}
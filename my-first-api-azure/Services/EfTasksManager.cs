using Microsoft.EntityFrameworkCore;
using my_first_api_azure.Data;
using my_first_api_azure.Models;

namespace my_first_api_azure.Services
{
    public class EfTasksManager : ITasksManager
    {
        private readonly TasksDbContext _context;

        public EfTasksManager(TasksDbContext context)
        {
            _context = context;
        }

        public async Task<List<TaskModel>> GetTasksByCreator(string createdBy)
        {
            var tasks = await _context.Tasks
                .Where(t => t.TaskCreatedBy == createdBy)
                .ToListAsync();

            foreach (var task in tasks)
                task.IsOverDue = !task.IsCompleted && task.TaskDueDate < DateTime.UtcNow;

            return tasks;
        }

        public async Task<TaskModel?> GetTaskById(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task != null)
                task.IsOverDue = !task.IsCompleted && task.TaskDueDate < DateTime.UtcNow;
            return task;
        }

        public async Task<Guid> CreateNewTask(string taskName, string createdBy, string assignedTo, DateTime dueDate)
        {
            var task = new TaskModel
            {
                TaskId = Guid.NewGuid(),
                TaskName = taskName,
                TaskCreatedBy = createdBy,
                TaskCreatedOn = DateTime.UtcNow,
                TaskDueDate = dueDate,
                TaskAssignedTo = assignedTo,
                IsCompleted = false
            };

            _context.Tasks.Add(task);
            await _context.SaveChangesAsync();
            return task.TaskId;
        }

        public async Task<bool> UpdateTask(Guid taskId, string taskName, string assignedTo, DateTime dueDate)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return false;

            task.TaskName = taskName;
            task.TaskAssignedTo = assignedTo;
            task.TaskDueDate = dueDate;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> MarkTaskCompleted(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return false;

            task.IsCompleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTask(Guid taskId)
        {
            var task = await _context.Tasks.FindAsync(taskId);
            if (task == null) return false;

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace my_first_api_azure.Data
{
    public class TasksDbContextFactory : IDesignTimeDbContextFactory<TasksDbContext>
    {
        public TasksDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddUserSecrets<TasksDbContext>()
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TasksDbContext>();
            var connectionString = configuration.GetConnectionString("TasksDb");
            optionsBuilder.UseNpgsql(connectionString);

            return new TasksDbContext(optionsBuilder.Options);
        }
    }
}
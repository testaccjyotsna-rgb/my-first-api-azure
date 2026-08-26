using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace my_first_api_azure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<Guid>(type: "uuid", nullable: false),
                    TaskName = table.Column<string>(type: "text", nullable: false),
                    TaskCreatedBy = table.Column<string>(type: "text", nullable: false),
                    TaskCreatedOn = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskDueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TaskAssignedTo = table.Column<string>(type: "text", nullable: false),
                    IsCompleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");
        }
    }
}

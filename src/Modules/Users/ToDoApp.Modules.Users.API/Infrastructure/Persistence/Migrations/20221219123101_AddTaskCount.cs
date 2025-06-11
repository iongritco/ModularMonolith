using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToDoApp.Modules.Users.Persistence.Migrations;

/// <inheritdoc />
public partial class AddTaskCount : Migration
{
	/// <inheritdoc />
	protected override void Up(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.AddColumn<int>(
			name: "NumberOfCompletedTasks",
			schema: "users",
			table: "AspNetUsers",
			type: "int",
			nullable: false,
			defaultValue: 0);
	}

	/// <inheritdoc />
	protected override void Down(MigrationBuilder migrationBuilder)
	{
		migrationBuilder.DropColumn(
			name: "NumberOfCompletedTasks",
			schema: "users",
			table: "AspNetUsers");
	}
}

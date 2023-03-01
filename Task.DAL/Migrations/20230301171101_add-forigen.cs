using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Task.DAL.Migrations
{
    /// <inheritdoc />
    public partial class addforigen : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Task",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Task_UsersId",
                table: "Task",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Task_Users_UsersId",
                table: "Task",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Task_Users_UsersId",
                table: "Task");

            migrationBuilder.DropIndex(
                name: "IX_Task_UsersId",
                table: "Task");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Task");
        }
    }
}

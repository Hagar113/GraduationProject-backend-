using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class v8 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "emp_Id",
                table: "Holidays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_emp_Id",
                table: "Holidays",
                column: "emp_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Employees_emp_Id",
                table: "Holidays",
                column: "emp_Id",
                principalTable: "Employees",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Employees_emp_Id",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_emp_Id",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "emp_Id",
                table: "Holidays");
        }
    }
}

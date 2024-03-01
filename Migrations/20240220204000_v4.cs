using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class v4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Companies_C_Id",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_C_Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "C_Id",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "Company_Id",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Company_Id",
                table: "Departments",
                column: "Company_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_Company_Id",
                table: "Departments",
                column: "Company_Id",
                principalTable: "Companies",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_Company_Id",
                table: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_Company_Id",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "Company_Id",
                table: "Departments");

            migrationBuilder.AddColumn<int>(
                name: "C_Id",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_C_Id",
                table: "Employees",
                column: "C_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Companies_C_Id",
                table: "Employees",
                column: "C_Id",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
        }
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
    }
}

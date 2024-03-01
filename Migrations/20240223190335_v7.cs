using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class v7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Companies_CompanyId",
                table: "Holidays");

            migrationBuilder.DropIndex(
                name: "IX_Holidays_CompanyId",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "C_Id",
                table: "Holidays");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "Holidays");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "C_Id",
                table: "Holidays",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CompanyId",
                table: "Holidays",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Holidays_CompanyId",
                table: "Holidays",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Companies_CompanyId",
                table: "Holidays",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}

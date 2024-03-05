using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class h3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "method",
                table: "generalSettings");

            migrationBuilder.RenameColumn(
                name: "selectedSecondWeekendDay",
                table: "generalSettings",
                newName: "SelectedSecondWeekendDay");

            migrationBuilder.RenameColumn(
                name: "selectedFirstWeekendDay",
                table: "generalSettings",
                newName: "SelectedFirstWeekendDay");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "generalSettings",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "SelectedSecondWeekendDay",
                table: "generalSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SelectedFirstWeekendDay",
                table: "generalSettings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Addition",
                table: "generalSettings",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Deduction",
                table: "generalSettings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Addition",
                table: "generalSettings");

            migrationBuilder.DropColumn(
                name: "Deduction",
                table: "generalSettings");

            migrationBuilder.RenameColumn(
                name: "SelectedSecondWeekendDay",
                table: "generalSettings",
                newName: "selectedSecondWeekendDay");

            migrationBuilder.RenameColumn(
                name: "SelectedFirstWeekendDay",
                table: "generalSettings",
                newName: "selectedFirstWeekendDay");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "generalSettings",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "selectedSecondWeekendDay",
                table: "generalSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "selectedFirstWeekendDay",
                table: "generalSettings",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "method",
                table: "generalSettings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GraduationProject.Migrations
{
    /// <inheritdoc />
    public partial class v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_Company_Id",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Companies_CompanyId",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Pages_Page_Id",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_Role_Id",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Role_Id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Role_Id",
                table: "Users",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Role_Id",
                table: "RolePermissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Page_Id",
                table: "RolePermissions",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Holidays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "C_Id",
                table: "Holidays",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Company_Id",
                table: "Departments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_Company_Id",
                table: "Departments",
                column: "Company_Id",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Companies_CompanyId",
                table: "Holidays",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Pages_Page_Id",
                table: "RolePermissions",
                column: "Page_Id",
                principalTable: "Pages",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_Role_Id",
                table: "RolePermissions",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Role_Id",
                table: "Users",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Companies_Company_Id",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Holidays_Companies_CompanyId",
                table: "Holidays");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Pages_Page_Id",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_RolePermissions_Roles_Role_Id",
                table: "RolePermissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Roles_Role_Id",
                table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Role_Id",
                table: "Users",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Role_Id",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Page_Id",
                table: "RolePermissions",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CompanyId",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "C_Id",
                table: "Holidays",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Company_Id",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Companies_Company_Id",
                table: "Departments",
                column: "Company_Id",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Holidays_Companies_CompanyId",
                table: "Holidays",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Pages_Page_Id",
                table: "RolePermissions",
                column: "Page_Id",
                principalTable: "Pages",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RolePermissions_Roles_Role_Id",
                table: "RolePermissions",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Roles_Role_Id",
                table: "Users",
                column: "Role_Id",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

﻿// <auto-generated />
using System;
using GraduationProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GraduationProject.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240320082538_bbbb")]
    partial class bbbb
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GraduationProject.Models.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("GeneralSettingsId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GeneralSettingsId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("GraduationProject.Models.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Company_Id")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Company_Id");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("GraduationProject.Models.Employee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AttendanceTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateOnly>("Birthdate")
                        .HasColumnType("date");

                    b.Property<DateOnly>("Contractdate")
                        .HasColumnType("date");

                    b.Property<int?>("GId")
                        .HasColumnType("int");

                    b.Property<string>("LeaveTime")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NationalId")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nationality")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Sal_ID")
                        .HasColumnType("int");

                    b.Property<int?>("deptid")
                        .HasColumnType("int");

                    b.Property<string>("phone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("user_Id")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GId");

                    b.HasIndex("Sal_ID");

                    b.HasIndex("deptid");

                    b.HasIndex("user_Id");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("GraduationProject.Models.EmployeeAttendance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Attendence")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("datetime2");

                    b.Property<int>("EmployeeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmployeeId");

                    b.ToTable("EmployeeAttendances");
                });

            modelBuilder.Entity("GraduationProject.Models.Gender", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("GName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Genders");
                });

            modelBuilder.Entity("GraduationProject.Models.GeneralSettings", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("Addition")
                        .HasColumnType("int");

                    b.Property<int?>("Deduction")
                        .HasColumnType("int");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SelectedFirstWeekendDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SelectedSecondWeekendDay")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("generalSettings");
                });

            modelBuilder.Entity("GraduationProject.Models.Holiday", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("C_Id")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Holidays");
                });

            modelBuilder.Entity("GraduationProject.Models.Page", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<string>("activeRoute")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("icon")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("label")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("routerLink")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.ToTable("Pages");
                });

            modelBuilder.Entity("GraduationProject.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("GraduationProject.Models.RolePermission", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("id"));

                    b.Property<bool>("IsAdd")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("bit");

                    b.Property<bool>("IsEdit")
                        .HasColumnType("bit");

                    b.Property<bool>("IsView")
                        .HasColumnType("bit");

                    b.Property<int?>("Page_Id")
                        .HasColumnType("int");

                    b.Property<int?>("Role_Id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("Page_Id");

                    b.HasIndex("Role_Id");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("GraduationProject.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<double?>("ExtraSalary")
                        .HasColumnType("float");

                    b.Property<double>("NetSalary")
                        .HasColumnType("float");

                    b.Property<double?>("SalaryLoss")
                        .HasColumnType("float");

                    b.Property<double?>("TotalSalary")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Salaries");
                });

            modelBuilder.Entity("GraduationProject.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("RefreshTokenExpiryTime")
                        .HasColumnType("datetime2");

                    b.Property<int?>("Role_Id")
                        .HasColumnType("int");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("Role_Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("GraduationProject.Models.Company", b =>
                {
                    b.HasOne("GraduationProject.Models.GeneralSettings", "GeneralSettings")
                        .WithMany()
                        .HasForeignKey("GeneralSettingsId");

                    b.Navigation("GeneralSettings");
                });

            modelBuilder.Entity("GraduationProject.Models.Department", b =>
                {
                    b.HasOne("GraduationProject.Models.Company", "company")
                        .WithMany("Dpartments")
                        .HasForeignKey("Company_Id");

                    b.Navigation("company");
                });

            modelBuilder.Entity("GraduationProject.Models.Employee", b =>
                {
                    b.HasOne("GraduationProject.Models.Gender", "gender")
                        .WithMany()
                        .HasForeignKey("GId");

                    b.HasOne("GraduationProject.Models.Salary", "salary")
                        .WithMany()
                        .HasForeignKey("Sal_ID");

                    b.HasOne("GraduationProject.Models.Department", "dept")
                        .WithMany("Employees")
                        .HasForeignKey("deptid");

                    b.HasOne("GraduationProject.Models.User", "user")
                        .WithMany()
                        .HasForeignKey("user_Id");

                    b.Navigation("dept");

                    b.Navigation("gender");

                    b.Navigation("salary");

                    b.Navigation("user");
                });

            modelBuilder.Entity("GraduationProject.Models.EmployeeAttendance", b =>
                {
                    b.HasOne("GraduationProject.Models.Employee", "Employee")
                        .WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Employee");
                });

            modelBuilder.Entity("GraduationProject.Models.Holiday", b =>
                {
                    b.HasOne("GraduationProject.Models.Company", "Company")
                        .WithMany("Holidays")
                        .HasForeignKey("CompanyId");

                    b.Navigation("Company");
                });

            modelBuilder.Entity("GraduationProject.Models.RolePermission", b =>
                {
                    b.HasOne("GraduationProject.Models.Page", "page")
                        .WithMany("RolePermissions")
                        .HasForeignKey("Page_Id");

                    b.HasOne("GraduationProject.Models.Role", "role")
                        .WithMany("RolesPermissions")
                        .HasForeignKey("Role_Id");

                    b.Navigation("page");

                    b.Navigation("role");
                });

            modelBuilder.Entity("GraduationProject.Models.User", b =>
                {
                    b.HasOne("GraduationProject.Models.Role", "role")
                        .WithMany()
                        .HasForeignKey("Role_Id");

                    b.Navigation("role");
                });

            modelBuilder.Entity("GraduationProject.Models.Company", b =>
                {
                    b.Navigation("Dpartments");

                    b.Navigation("Holidays");
                });

            modelBuilder.Entity("GraduationProject.Models.Department", b =>
                {
                    b.Navigation("Employees");
                });

            modelBuilder.Entity("GraduationProject.Models.Page", b =>
                {
                    b.Navigation("RolePermissions");
                });

            modelBuilder.Entity("GraduationProject.Models.Role", b =>
                {
                    b.Navigation("RolesPermissions");
                });
#pragma warning restore 612, 618
        }
    }
}

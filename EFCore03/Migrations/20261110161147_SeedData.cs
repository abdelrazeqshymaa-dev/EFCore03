using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EFCore03.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "CategoryId", "Description", "InstructorId", "IsPublished", "Level", "Price", "PublishedDate", "Title" },
                values: new object[,]
                {
                    { 1, 3, null, 1, true, "Beginner", 99.99m, new DateTime(2024, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "ASP.NET Core Basics" },
                    { 2, 2, null, 1, false, "Advanced", 149.50m, null, "Advanced EF Core" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "DateOfBirth", "Email", "FullName" },
                values: new object[,]
                {
                    { 1, new DateTime(2000, 5, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "omar@mail.com", "Omar Khaled" },
                    { 2, new DateTime(2001, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "mona@mail.com", "Mona Samir" }
                });

            migrationBuilder.InsertData(
                table: "Enrollments",
                columns: new[] { "CourseId", "StudentId", "EnrollmentDate", "FinalGrade", "Progress", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2024, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 40, "Active" },
                    { 1, 2, new DateTime(2024, 7, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 92.5m, 100, "Completed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "Enrollments",
                keyColumns: new[] { "CourseId", "StudentId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}

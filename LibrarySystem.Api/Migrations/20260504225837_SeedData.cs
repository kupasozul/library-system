using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibrarySystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookNumber", "Author", "PublicationYear", "Publisher", "Title" },
                values: new object[,]
                {
                    { 1, "Molnár Ferenc", 1907, "Móra", "A Pál utcai fiúk" },
                    { 2, "Gárdonyi Géza", 1901, "Dante", "Egri csillagok" },
                    { 3, "Robert C. Martin", 2008, "Prentice Hall", "Clean Code" }
                });

            migrationBuilder.InsertData(
                table: "Readers",
                columns: new[] { "ReaderNumber", "Address", "DateOfBirth", "Name" },
                values: new object[,]
                {
                    { 1, "4400 Nyíregyháza, Kossuth tér 1-3.", new DateTime(1990, 4, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Tóth Tibor" },
                    { 2, "4024 Debrecen, Piac utca 20.", new DateTime(2005, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Takács Éva" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookNumber",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookNumber",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookNumber",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "ReaderNumber",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Readers",
                keyColumn: "ReaderNumber",
                keyValue: 2);
        }
    }
}

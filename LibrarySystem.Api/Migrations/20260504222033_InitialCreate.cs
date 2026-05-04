using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibrarySystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    BookNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Author = table.Column<string>(type: "TEXT", nullable: false),
                    Publisher = table.Column<string>(type: "TEXT", nullable: false),
                    PublicationYear = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.BookNumber);
                });

            migrationBuilder.CreateTable(
                name: "Readers",
                columns: table => new
                {
                    ReaderNumber = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Readers", x => x.ReaderNumber);
                });

            migrationBuilder.CreateTable(
                name: "Loans",
                columns: table => new
                {
                    LoanId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReaderNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    BookNumber = table.Column<int>(type: "INTEGER", nullable: false),
                    LoanDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReturnDeadline = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ReaderNumber1 = table.Column<int>(type: "INTEGER", nullable: true),
                    BookNumber1 = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Loans", x => x.LoanId);
                    table.ForeignKey(
                        name: "FK_Loans_Books_BookNumber1",
                        column: x => x.BookNumber1,
                        principalTable: "Books",
                        principalColumn: "BookNumber");
                    table.ForeignKey(
                        name: "FK_Loans_Readers_ReaderNumber1",
                        column: x => x.ReaderNumber1,
                        principalTable: "Readers",
                        principalColumn: "ReaderNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_BookNumber1",
                table: "Loans",
                column: "BookNumber1");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_ReaderNumber1",
                table: "Loans",
                column: "ReaderNumber1");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Loans");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Readers");
        }
    }
}

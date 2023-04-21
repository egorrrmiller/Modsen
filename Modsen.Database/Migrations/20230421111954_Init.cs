using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Modsen.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Isbn = table.Column<string>(type: "TEXT", maxLength: 17, nullable: false),
                    Title = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "TEXT", maxLength: 35, nullable: false),
                    BorrowTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2023, 4, 21, 14, 19, 54, 77, DateTimeKind.Utc).AddTicks(578)),
                    ReturnTime = table.Column<DateTime>(type: "TEXT", nullable: false, defaultValue: new DateTime(2023, 4, 28, 14, 19, 54, 77, DateTimeKind.Utc).AddTicks(928))
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                    table.UniqueConstraint("AK_Books_Isbn", x => x.Isbn);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Email = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    Password = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Email);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

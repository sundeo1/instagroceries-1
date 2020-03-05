using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Instagroceries.Migrations
{
    public partial class Initialcreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Test2s",
                columns: table => new
                {
                    Test2ID = table.Column<Guid>(nullable: false),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test2s", x => x.Test2ID);
                });

            migrationBuilder.CreateTable(
                name: "Test1s",
                columns: table => new
                {
                    Test1ID = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Test2ID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Test1s", x => x.Test1ID);
                    table.ForeignKey(
                        name: "FK_Test1s_Test2s_Test2ID",
                        column: x => x.Test2ID,
                        principalTable: "Test2s",
                        principalColumn: "Test2ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Test1s_Test2ID",
                table: "Test1s",
                column: "Test2ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Test1s");

            migrationBuilder.DropTable(
                name: "Test2s");
        }
    }
}

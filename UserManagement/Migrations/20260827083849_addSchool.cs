using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.Migrations
{
    /// <inheritdoc />
    public partial class addSchool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Classes",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    SchoolId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classes", x => x.id);
                    table.ForeignKey(
                        name: "FK_Classes_Schools_SchoolId",
                        column: x => x.SchoolId,
                        principalTable: "Schools",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ClassEntityUserEntities",
                columns: table => new
                {
                    Classesid = table.Column<Guid>(type: "uuid", nullable: false),
                    Usersid = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassEntityUserEntities", x => new { x.Classesid, x.Usersid });
                    table.ForeignKey(
                        name: "FK_ClassEntityUserEntities_Classes_Classesid",
                        column: x => x.Classesid,
                        principalTable: "Classes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClassEntityUserEntities_Users_Usersid",
                        column: x => x.Usersid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassEntityUserEntities_Usersid",
                table: "ClassEntityUserEntities",
                column: "Usersid");

            migrationBuilder.CreateIndex(
                name: "IX_Classes_SchoolId",
                table: "Classes",
                column: "SchoolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassEntityUserEntities");

            migrationBuilder.DropTable(
                name: "Classes");

            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}

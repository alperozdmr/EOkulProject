using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class updated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StudentNotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Math = table.Column<float>(type: "real", nullable: false),
                    Physics = table.Column<float>(type: "real", nullable: false),
                    Biology = table.Column<float>(type: "real", nullable: false),
                    Chemistry = table.Column<float>(type: "real", nullable: false),
                    Turkish = table.Column<float>(type: "real", nullable: false),
                    History = table.Column<float>(type: "real", nullable: false),
                    Geography = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentNotes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentNotes");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualNovelManager.Data.Migrations
{
    public partial class AddedVisualNovel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisualNovel",
                columns: table => new
                {
                    GameId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VndbId = table.Column<int>(type: "int", nullable: true),
                    GameTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GameAlias = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CompletionStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisualNovel", x => x.GameId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisualNovel");
        }
    }
}

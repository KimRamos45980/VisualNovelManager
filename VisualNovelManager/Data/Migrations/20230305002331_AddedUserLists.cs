using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualNovelManager.Data.Migrations
{
    public partial class AddedUserLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VNList",
                columns: table => new
                {
                    ListId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    List = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VNList", x => x.ListId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VNList");
        }
    }
}

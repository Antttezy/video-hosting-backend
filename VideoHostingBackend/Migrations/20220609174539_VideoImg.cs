using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoHostingBackend.Migrations
{
    public partial class VideoImg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CoverImg",
                table: "Videos",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoverImg",
                table: "Videos");
        }
    }
}

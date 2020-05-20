using Microsoft.EntityFrameworkCore.Migrations;

namespace SteamingService.Migrations
{
    public partial class AddBroadcastUri : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BroadcastUri",
                table: "Streams",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BroadcastUri",
                table: "Streams");
        }
    }
}

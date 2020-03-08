using Microsoft.EntityFrameworkCore.Migrations;

namespace SteamingService.Migrations
{
    public partial class AddStream : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StreamerKey",
                table: "Users",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Streams",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    BroadcasterId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Streams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Streams_Users_BroadcasterId",
                        column: x => x.BroadcasterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StreamViewer",
                columns: table => new
                {
                    StreamId = table.Column<int>(nullable: false),
                    ViewerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StreamViewer", x => new { x.StreamId, x.ViewerId });
                    table.ForeignKey(
                        name: "FK_StreamViewer_Streams_StreamId",
                        column: x => x.StreamId,
                        principalTable: "Streams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StreamViewer_Users_ViewerId",
                        column: x => x.ViewerId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Streams_BroadcasterId",
                table: "Streams",
                column: "BroadcasterId");

            migrationBuilder.CreateIndex(
                name: "IX_StreamViewer_ViewerId",
                table: "StreamViewer",
                column: "ViewerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StreamViewer");

            migrationBuilder.DropTable(
                name: "Streams");

            migrationBuilder.DropColumn(
                name: "StreamerKey",
                table: "Users");
        }
    }
}

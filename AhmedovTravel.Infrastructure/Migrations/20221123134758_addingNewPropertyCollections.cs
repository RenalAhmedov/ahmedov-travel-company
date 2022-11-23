using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedovTravel.Infrastructure.Migrations
{
    public partial class addingNewPropertyCollections : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Destinations_DestinationId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_DestinationId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DestinationId",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Transports",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transports_UserId",
                table: "Transports",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transports_AspNetUsers_UserId",
                table: "Transports",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transports_AspNetUsers_UserId",
                table: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Transports_UserId",
                table: "Transports");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Transports");

            migrationBuilder.AddColumn<int>(
                name: "DestinationId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DestinationId",
                table: "AspNetUsers",
                column: "DestinationId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Destinations_DestinationId",
                table: "AspNetUsers",
                column: "DestinationId",
                principalTable: "Destinations",
                principalColumn: "Id");
        }
    }
}

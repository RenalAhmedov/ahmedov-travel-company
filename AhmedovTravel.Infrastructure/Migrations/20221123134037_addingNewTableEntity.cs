using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedovTravel.Infrastructure.Migrations
{
    public partial class addingNewTableEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TransportId",
                table: "Destinations",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransportType = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "ImageUrl", "TransportType" },
                values: new object[] { 1, "https://media.istockphoto.com/id/492620954/photo/classical-red-bus.jpg?s=612x612&w=0&k=20&c=U2P9mlO8D7xZCYjRfifEkWxdUHp7JH7XPBn2dB1c9Qs=", "Bus" });

            migrationBuilder.InsertData(
                table: "Transports",
                columns: new[] { "Id", "ImageUrl", "TransportType" },
                values: new object[] { 2, "https://media.istockphoto.com/id/155439315/photo/passenger-airplane-flying-above-clouds-during-sunset.jpg?s=612x612&w=0&k=20&c=LJWadbs3B-jSGJBVy9s0f8gZMHi2NvWFXa3VJ2lFcL0=", "Airplane" });

            migrationBuilder.CreateIndex(
                name: "IX_Destinations_TransportId",
                table: "Destinations",
                column: "TransportId");

            migrationBuilder.AddForeignKey(
                name: "FK_Destinations_Transports_TransportId",
                table: "Destinations",
                column: "TransportId",
                principalTable: "Transports",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Destinations_Transports_TransportId",
                table: "Destinations");

            migrationBuilder.DropTable(
                name: "Transports");

            migrationBuilder.DropIndex(
                name: "IX_Destinations_TransportId",
                table: "Destinations");

            migrationBuilder.DropColumn(
                name: "TransportId",
                table: "Destinations");
        }
    }
}

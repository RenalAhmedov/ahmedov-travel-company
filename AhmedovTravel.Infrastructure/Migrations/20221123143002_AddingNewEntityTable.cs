using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedovTravel.Infrastructure.Migrations
{
    public partial class AddingNewEntityTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoomServiceId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "RoomServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricePerPerson = table.Column<decimal>(type: "money", precision: 18, scale: 2, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(75)", maxLength: 75, nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomServices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "RoomServices",
                columns: new[] { "Id", "Description", "ImageUrl", "PricePerPerson", "UserId" },
                values: new object[] { 1, "A Small Pizza, with small french fries, small chicken nuggets portion and a bottle of Coca-Cola 350ml.", "https://store.irabwah.com/image/cache/catalog/Chicken%20Tikka%20Pizza,%20Nuggets,%20Fries%20and%20Coke/Chicken%20tikka%20pizza%20(1)-500x500.jpg", 15m, null });

            migrationBuilder.InsertData(
                table: "RoomServices",
                columns: new[] { "Id", "Description", "ImageUrl", "PricePerPerson", "UserId" },
                values: new object[] { 2, "A Beef Burger, with medium french fries, and a Coca-Cola 250ml", "https://media.istockphoto.com/id/1344002306/photo/delicious-cheeseburger-with-cola-and-potato-fries-on-the-white-background-fast-food-concept.jpg?s=612x612&w=0&k=20&c=B8kZWz6zqmB11e4bIYt5rJ0U9aQ21AfZGgvT_JPIxqA=", 20m, null });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomServiceId",
                table: "Rooms",
                column: "RoomServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomServices_UserId",
                table: "RoomServices",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomServices_RoomServiceId",
                table: "Rooms",
                column: "RoomServiceId",
                principalTable: "RoomServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomServices_RoomServiceId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomServices");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_RoomServiceId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "RoomServiceId",
                table: "Rooms");
        }
    }
}

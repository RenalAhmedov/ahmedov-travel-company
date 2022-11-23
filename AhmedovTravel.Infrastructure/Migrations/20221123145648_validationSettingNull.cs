using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedovTravel.Infrastructure.Migrations
{
    public partial class validationSettingNull : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomServices_RoomServiceId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "RoomServiceId",
                table: "Rooms",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomServices_RoomServiceId",
                table: "Rooms",
                column: "RoomServiceId",
                principalTable: "RoomServices",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_RoomServices_RoomServiceId",
                table: "Rooms");

            migrationBuilder.AlterColumn<int>(
                name: "RoomServiceId",
                table: "Rooms",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_RoomServices_RoomServiceId",
                table: "Rooms",
                column: "RoomServiceId",
                principalTable: "RoomServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

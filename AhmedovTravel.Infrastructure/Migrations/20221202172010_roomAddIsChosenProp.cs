using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedovTravel.Infrastructure.Migrations
{
    public partial class roomAddIsChosenProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsChosen",
                table: "Rooms",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsChosen",
                table: "Rooms");
        }
    }
}

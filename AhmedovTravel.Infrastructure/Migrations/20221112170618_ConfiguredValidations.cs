using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AhmedovTravel.Infrastructure.Migrations
{
    public partial class ConfiguredValidations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isChosen",
                table: "Destinations",
                newName: "IsChosen");

            migrationBuilder.AlterColumn<bool>(
                name: "IsChosen",
                table: "Destinations",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsChosen",
                table: "Destinations",
                newName: "isChosen");

            migrationBuilder.AlterColumn<bool>(
                name: "isChosen",
                table: "Destinations",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false);
        }
    }
}

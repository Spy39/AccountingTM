using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class initEnd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Storages");

            migrationBuilder.DropColumn(
                name: "HoursAfterRepair",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "InitialOperatingTime",
                table: "Repairs");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "CompletedWorks");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Storages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HoursAfterRepair",
                table: "Repairs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "InitialOperatingTime",
                table: "Repairs",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "CompletedWorks",
                type: "text",
                nullable: true);
        }
    }
}

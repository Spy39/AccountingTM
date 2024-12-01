using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class Set : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Employees_EmployeeId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Locations_LocationId",
                table: "Sets");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Sets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Sets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Employees_EmployeeId",
                table: "Sets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Locations_LocationId",
                table: "Sets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Employees_EmployeeId",
                table: "Sets");

            migrationBuilder.DropForeignKey(
                name: "FK_Sets_Locations_LocationId",
                table: "Sets");

            migrationBuilder.AlterColumn<int>(
                name: "LocationId",
                table: "Sets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Sets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Employees_EmployeeId",
                table: "Sets",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sets_Locations_LocationId",
                table: "Sets",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

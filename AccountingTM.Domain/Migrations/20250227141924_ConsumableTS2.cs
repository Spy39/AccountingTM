using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class ConsumableTS2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableHistories_Brands_EmployeeId",
                table: "ConsumableHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableHistories_Employees_EmployeeId",
                table: "ConsumableHistories",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableHistories_Employees_EmployeeId",
                table: "ConsumableHistories");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableHistories_Brands_EmployeeId",
                table: "ConsumableHistories",
                column: "EmployeeId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

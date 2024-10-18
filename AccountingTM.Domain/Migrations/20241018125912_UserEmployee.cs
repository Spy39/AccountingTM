using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class UserEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "ConsumableHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableHistories_EmployeeId",
                table: "ConsumableHistories",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableHistories_Brands_EmployeeId",
                table: "ConsumableHistories",
                column: "EmployeeId",
                principalTable: "Brands",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableHistories_Brands_EmployeeId",
                table: "ConsumableHistories");

            migrationBuilder.DropIndex(
                name: "IX_ConsumableHistories_EmployeeId",
                table: "ConsumableHistories");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "ConsumableHistories");
        }
    }
}

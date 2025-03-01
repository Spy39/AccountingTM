using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class ConsumableTS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "Consumables",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "ConsumableHistories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Consumables_TechnicalEquipmentId",
                table: "Consumables",
                column: "TechnicalEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ConsumableHistories_TechnicalEquipmentId",
                table: "ConsumableHistories",
                column: "TechnicalEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConsumableHistories_TechnicalEquipment_TechnicalEquipmentId",
                table: "ConsumableHistories",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Consumables_TechnicalEquipment_TechnicalEquipmentId",
                table: "Consumables",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConsumableHistories_TechnicalEquipment_TechnicalEquipmentId",
                table: "ConsumableHistories");

            migrationBuilder.DropForeignKey(
                name: "FK_Consumables_TechnicalEquipment_TechnicalEquipmentId",
                table: "Consumables");

            migrationBuilder.DropIndex(
                name: "IX_Consumables_TechnicalEquipmentId",
                table: "Consumables");

            migrationBuilder.DropIndex(
                name: "IX_ConsumableHistories_TechnicalEquipmentId",
                table: "ConsumableHistories");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "Consumables");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "ConsumableHistories");
        }
    }
}

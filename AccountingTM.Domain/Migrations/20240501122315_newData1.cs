using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class newData1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "Storage",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "Repair",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "ReceptionAndTransmission",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "DisposalInformation",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TechnicalEquipmentId",
                table: "CompletedWork",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Storage_TechnicalEquipmentId",
                table: "Storage",
                column: "TechnicalEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Repair_TechnicalEquipmentId",
                table: "Repair",
                column: "TechnicalEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ReceptionAndTransmission_TechnicalEquipmentId",
                table: "ReceptionAndTransmission",
                column: "TechnicalEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DisposalInformation_TechnicalEquipmentId",
                table: "DisposalInformation",
                column: "TechnicalEquipmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWork_TechnicalEquipmentId",
                table: "CompletedWork",
                column: "TechnicalEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWork_TechnicalEquipment_TechnicalEquipmentId",
                table: "CompletedWork",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisposalInformation_TechnicalEquipment_TechnicalEquipmentId",
                table: "DisposalInformation",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionAndTransmission_TechnicalEquipment_TechnicalEquipm~",
                table: "ReceptionAndTransmission",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repair_TechnicalEquipment_TechnicalEquipmentId",
                table: "Repair",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storage_TechnicalEquipment_TechnicalEquipmentId",
                table: "Storage",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWork_TechnicalEquipment_TechnicalEquipmentId",
                table: "CompletedWork");

            migrationBuilder.DropForeignKey(
                name: "FK_DisposalInformation_TechnicalEquipment_TechnicalEquipmentId",
                table: "DisposalInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionAndTransmission_TechnicalEquipment_TechnicalEquipm~",
                table: "ReceptionAndTransmission");

            migrationBuilder.DropForeignKey(
                name: "FK_Repair_TechnicalEquipment_TechnicalEquipmentId",
                table: "Repair");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_TechnicalEquipment_TechnicalEquipmentId",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_Storage_TechnicalEquipmentId",
                table: "Storage");

            migrationBuilder.DropIndex(
                name: "IX_Repair_TechnicalEquipmentId",
                table: "Repair");

            migrationBuilder.DropIndex(
                name: "IX_ReceptionAndTransmission_TechnicalEquipmentId",
                table: "ReceptionAndTransmission");

            migrationBuilder.DropIndex(
                name: "IX_DisposalInformation_TechnicalEquipmentId",
                table: "DisposalInformation");

            migrationBuilder.DropIndex(
                name: "IX_CompletedWork_TechnicalEquipmentId",
                table: "CompletedWork");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "Storage");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "Repair");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "ReceptionAndTransmission");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "DisposalInformation");

            migrationBuilder.DropColumn(
                name: "TechnicalEquipmentId",
                table: "CompletedWork");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class Tables0705242 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Indicators_IndicatorsId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Units_UnitsId",
                table: "Characteristics");

            migrationBuilder.RenameColumn(
                name: "UnitsId",
                table: "Characteristics",
                newName: "UnitId");

            migrationBuilder.RenameColumn(
                name: "IndicatorsId",
                table: "Characteristics",
                newName: "IndicatorId");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_UnitsId",
                table: "Characteristics",
                newName: "IX_Characteristics_UnitId");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_IndicatorsId",
                table: "Characteristics",
                newName: "IX_Characteristics_IndicatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Indicators_IndicatorId",
                table: "Characteristics",
                column: "IndicatorId",
                principalTable: "Indicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Units_UnitId",
                table: "Characteristics",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Indicators_IndicatorId",
                table: "Characteristics");

            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Units_UnitId",
                table: "Characteristics");

            migrationBuilder.RenameColumn(
                name: "UnitId",
                table: "Characteristics",
                newName: "UnitsId");

            migrationBuilder.RenameColumn(
                name: "IndicatorId",
                table: "Characteristics",
                newName: "IndicatorsId");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_UnitId",
                table: "Characteristics",
                newName: "IX_Characteristics_UnitsId");

            migrationBuilder.RenameIndex(
                name: "IX_Characteristics_IndicatorId",
                table: "Characteristics",
                newName: "IX_Characteristics_IndicatorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Indicators_IndicatorsId",
                table: "Characteristics",
                column: "IndicatorsId",
                principalTable: "Indicators",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Units_UnitsId",
                table: "Characteristics",
                column: "UnitsId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

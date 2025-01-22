using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DocumentType");

            migrationBuilder.DropColumn(
                name: "Model",
                table: "TechnicalEquipment");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "TechnicalEquipment",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_ModelId",
                table: "TechnicalEquipment",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_Models_ModelId",
                table: "TechnicalEquipment",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Models_ModelId",
                table: "TechnicalEquipment");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_ModelId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "TechnicalEquipment");

            migrationBuilder.AddColumn<string>(
                name: "Model",
                table: "TechnicalEquipment",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "DocumentType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });
        }
    }
}

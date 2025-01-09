using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class tableModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Descriptions");

            migrationBuilder.AddColumn<int>(
                name: "ModelId",
                table: "Characteristics",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Models",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Models", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_ModelId",
                table: "Characteristics",
                column: "ModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Characteristics_Models_ModelId",
                table: "Characteristics",
                column: "ModelId",
                principalTable: "Models",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Characteristics_Models_ModelId",
                table: "Characteristics");

            migrationBuilder.DropTable(
                name: "Models");

            migrationBuilder.DropIndex(
                name: "IX_Characteristics_ModelId",
                table: "Characteristics");

            migrationBuilder.DropColumn(
                name: "ModelId",
                table: "Characteristics");

            migrationBuilder.CreateTable(
                name: "Descriptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TechnicalEquipmentId = table.Column<int>(type: "integer", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Descriptions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Descriptions_TechnicalEquipment_TechnicalEquipmentId",
                        column: x => x.TechnicalEquipmentId,
                        principalTable: "TechnicalEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Descriptions_TechnicalEquipmentId",
                table: "Descriptions",
                column: "TechnicalEquipmentId");
        }
    }
}

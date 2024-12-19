using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class SetHistory12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SetId",
                table: "SetHistories",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_SetHistories_SetId",
                table: "SetHistories",
                column: "SetId");

            migrationBuilder.AddForeignKey(
                name: "FK_SetHistories_Sets_SetId",
                table: "SetHistories",
                column: "SetId",
                principalTable: "Sets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SetHistories_Sets_SetId",
                table: "SetHistories");

            migrationBuilder.DropIndex(
                name: "IX_SetHistories_SetId",
                table: "SetHistories");

            migrationBuilder.DropColumn(
                name: "SetId",
                table: "SetHistories");
        }
    }
}

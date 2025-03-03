using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class Comments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "CommentsOnTheApplications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CommentsOnTheApplications_EmployeeId",
                table: "CommentsOnTheApplications",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentsOnTheApplications_Employees_EmployeeId",
                table: "CommentsOnTheApplications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentsOnTheApplications_Employees_EmployeeId",
                table: "CommentsOnTheApplications");

            migrationBuilder.DropIndex(
                name: "IX_CommentsOnTheApplications_EmployeeId",
                table: "CommentsOnTheApplications");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "CommentsOnTheApplications");
        }
    }
}

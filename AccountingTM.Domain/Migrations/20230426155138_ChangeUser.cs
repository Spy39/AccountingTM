using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class ChangeUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Permission",
                table: "Users",
                newName: "Role");

            migrationBuilder.RenameColumn(
                name: "PasswordUser",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "LoginUser",
                table: "Users",
                newName: "Login");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Role",
                table: "Users",
                newName: "Permission");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordUser");

            migrationBuilder.RenameColumn(
                name: "Login",
                table: "Users",
                newName: "LoginUser");
        }
    }
}

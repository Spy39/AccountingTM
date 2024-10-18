using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class AddSetHistories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfOperation",
                table: "Sets");

            migrationBuilder.DropColumn(
                name: "TypeOfOperation",
                table: "Sets");

			migrationBuilder.Sql("ALTER TABLE \"ConsumableHistories\" ALTER COLUMN \"Quantity\" TYPE double precision USING \"Quantity\"::double precision;");

		}

		protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfOperation",
                table: "Sets",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TypeOfOperation",
                table: "Sets",
                type: "text",
                nullable: true);

			migrationBuilder.Sql("ALTER TABLE \"ConsumableHistories\" ALTER COLUMN \"Quantity\" TYPE text USING \"Quantity\"::text;");

		}
	}
}

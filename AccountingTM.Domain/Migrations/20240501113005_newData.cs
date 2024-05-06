using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class newData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Staff_ConveyedId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Staff_NewRespId",
                table: "Moving");

            migrationBuilder.DropTable(
                name: "BrandTypeTM");

            migrationBuilder.DropTable(
                name: "Staff");

            migrationBuilder.DropTable(
                name: "TypeTM");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TechnicalEquipment");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "TechnicalEquipment",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Responsible",
                table: "TechnicalEquipment",
                newName: "InventoryNumber");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "TechnicalEquipment",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BrandId",
                table: "TechnicalEquipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "TechnicalEquipment",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateEnd",
                table: "TechnicalEquipment",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateGarant",
                table: "TechnicalEquipment",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateStart",
                table: "TechnicalEquipment",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "TechnicalEquipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "TechnicalEquipment",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "TechnicalEquipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "State",
                table: "TechnicalEquipment",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Brand",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisposalInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisposalInformation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    FatherName = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indicators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indicators", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ReceptionAndTransmission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ProductCondition = table.Column<string>(type: "text", nullable: false),
                    Base = table.Column<string>(type: "text", nullable: false),
                    Passed = table.Column<string>(type: "text", nullable: false),
                    Accepted = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReceptionAndTransmission", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Repair",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Company = table.Column<string>(type: "text", nullable: false),
                    InitialOperatingTime = table.Column<string>(type: "text", nullable: false),
                    HoursAfterRepair = table.Column<string>(type: "text", nullable: false),
                    ReasonForRepair = table.Column<string>(type: "text", nullable: false),
                    RepairInformation = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repair", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Storage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Acceptance = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Removal = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StorageConditions = table.Column<string>(type: "text", nullable: false),
                    TypeOfStorage = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Storage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeEquipment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEquipment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Conservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TechnicalEquipmentId = table.Column<int>(type: "integer", nullable: false),
                    EmployeeId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NameOfWorks = table.Column<string>(type: "text", nullable: false),
                    Validity = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Conservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Conservation_Employee_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Conservation_TechnicalEquipment_TechnicalEquipmentId",
                        column: x => x.TechnicalEquipmentId,
                        principalTable: "TechnicalEquipment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Applications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationNumber = table.Column<string>(type: "text", nullable: false),
                    DateOfCreation = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DateOfChange = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ExpirationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Author = table.Column<string>(type: "text", nullable: false),
                    Executor = table.Column<string>(type: "text", nullable: true),
                    LastReply = table.Column<string>(type: "text", nullable: true),
                    LocationId = table.Column<int>(type: "integer", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Applications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Applications_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Applications_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Characteristics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IndicatorsId = table.Column<int>(type: "integer", nullable: false),
                    UnitsId = table.Column<int>(type: "integer", nullable: false),
                    Meaning = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Characteristics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Characteristics_Indicators_IndicatorsId",
                        column: x => x.IndicatorsId,
                        principalTable: "Indicators",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Characteristics_Units_UnitsId",
                        column: x => x.UnitsId,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompletedWork",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ApplicationsId = table.Column<int>(type: "integer", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NameAndReason = table.Column<string>(type: "text", nullable: false),
                    Completed = table.Column<string>(type: "text", nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompletedWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompletedWork_Applications_ApplicationsId",
                        column: x => x.ApplicationsId,
                        principalTable: "Applications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_BrandId",
                table: "TechnicalEquipment",
                column: "BrandId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_EmployeeId",
                table: "TechnicalEquipment",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_LocationId",
                table: "TechnicalEquipment",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TechnicalEquipment_TypeId",
                table: "TechnicalEquipment",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_CategoryId",
                table: "Applications",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Applications_LocationId",
                table: "Applications",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_IndicatorsId",
                table: "Characteristics",
                column: "IndicatorsId");

            migrationBuilder.CreateIndex(
                name: "IX_Characteristics_UnitsId",
                table: "Characteristics",
                column: "UnitsId");

            migrationBuilder.CreateIndex(
                name: "IX_CompletedWork_ApplicationsId",
                table: "CompletedWork",
                column: "ApplicationsId");

            migrationBuilder.CreateIndex(
                name: "IX_Conservation_EmployeeId",
                table: "Conservation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Conservation_TechnicalEquipmentId",
                table: "Conservation",
                column: "TechnicalEquipmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moving_Employee_ConveyedId",
                table: "Moving",
                column: "ConveyedId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moving_Employee_NewRespId",
                table: "Moving",
                column: "NewRespId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_Brand_BrandId",
                table: "TechnicalEquipment",
                column: "BrandId",
                principalTable: "Brand",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_Employee_EmployeeId",
                table: "TechnicalEquipment",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_Location_LocationId",
                table: "TechnicalEquipment",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_TypeEquipment_TypeId",
                table: "TechnicalEquipment",
                column: "TypeId",
                principalTable: "TypeEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Employee_ConveyedId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Employee_NewRespId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Brand_BrandId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Employee_EmployeeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Location_LocationId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_TypeEquipment_TypeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropTable(
                name: "Brand");

            migrationBuilder.DropTable(
                name: "Characteristics");

            migrationBuilder.DropTable(
                name: "CompletedWork");

            migrationBuilder.DropTable(
                name: "Conservation");

            migrationBuilder.DropTable(
                name: "DisposalInformation");

            migrationBuilder.DropTable(
                name: "ReceptionAndTransmission");

            migrationBuilder.DropTable(
                name: "Repair");

            migrationBuilder.DropTable(
                name: "Storage");

            migrationBuilder.DropTable(
                name: "TypeEquipment");

            migrationBuilder.DropTable(
                name: "Indicators");

            migrationBuilder.DropTable(
                name: "Units");

            migrationBuilder.DropTable(
                name: "Applications");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_BrandId",
                table: "TechnicalEquipment");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_EmployeeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_LocationId",
                table: "TechnicalEquipment");

            migrationBuilder.DropIndex(
                name: "IX_TechnicalEquipment_TypeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "BrandId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "DateEnd",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "DateGarant",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "DateStart",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "TechnicalEquipment");

            migrationBuilder.DropColumn(
                name: "State",
                table: "TechnicalEquipment");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "TechnicalEquipment",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "InventoryNumber",
                table: "TechnicalEquipment",
                newName: "Responsible");

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "TechnicalEquipment",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TechnicalEquipment",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TechnicalEquipment",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BrandTypeTM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandTypeTM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Staff",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FatherName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    Position = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staff", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TypeTM",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeTM", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Moving_Staff_ConveyedId",
                table: "Moving",
                column: "ConveyedId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moving_Staff_NewRespId",
                table: "Moving",
                column: "NewRespId",
                principalTable: "Staff",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

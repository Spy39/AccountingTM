using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccountingTM.Domain.Migrations
{
    public partial class directory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Location_LocationId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWork_Applications_ApplicationsId",
                table: "CompletedWork");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWork_TechnicalEquipment_TechnicalEquipmentId",
                table: "CompletedWork");

            migrationBuilder.DropForeignKey(
                name: "FK_Conservation_Employee_EmployeeId",
                table: "Conservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Conservation_TechnicalEquipment_TechnicalEquipmentId",
                table: "Conservation");

            migrationBuilder.DropForeignKey(
                name: "FK_DisposalInformation_TechnicalEquipment_TechnicalEquipmentId",
                table: "DisposalInformation");

            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Employee_ConveyedId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Employee_NewRespId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionAndTransmission_TechnicalEquipment_TechnicalEquipm~",
                table: "ReceptionAndTransmission");

            migrationBuilder.DropForeignKey(
                name: "FK_Repair_TechnicalEquipment_TechnicalEquipmentId",
                table: "Repair");

            migrationBuilder.DropForeignKey(
                name: "FK_Storage_TechnicalEquipment_TechnicalEquipmentId",
                table: "Storage");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Employee_EmployeeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Location_LocationId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_TypeEquipment_TypeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeEquipment",
                table: "TypeEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storage",
                table: "Storage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repair",
                table: "Repair");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceptionAndTransmission",
                table: "ReceptionAndTransmission");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Location",
                table: "Location");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employee",
                table: "Employee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisposalInformation",
                table: "DisposalInformation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conservation",
                table: "Conservation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedWork",
                table: "CompletedWork");

            migrationBuilder.RenameTable(
                name: "TypeEquipment",
                newName: "TypeEquipments");

            migrationBuilder.RenameTable(
                name: "Storage",
                newName: "Storages");

            migrationBuilder.RenameTable(
                name: "Repair",
                newName: "Repairs");

            migrationBuilder.RenameTable(
                name: "ReceptionAndTransmission",
                newName: "ReceptionAndTransmissions");

            migrationBuilder.RenameTable(
                name: "Location",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "Employee",
                newName: "Employees");

            migrationBuilder.RenameTable(
                name: "DisposalInformation",
                newName: "DisposalInformations");

            migrationBuilder.RenameTable(
                name: "Conservation",
                newName: "Conservations");

            migrationBuilder.RenameTable(
                name: "CompletedWork",
                newName: "CompletedWorks");

            migrationBuilder.RenameIndex(
                name: "IX_Storage_TechnicalEquipmentId",
                table: "Storages",
                newName: "IX_Storages_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Repair_TechnicalEquipmentId",
                table: "Repairs",
                newName: "IX_Repairs_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptionAndTransmission_TechnicalEquipmentId",
                table: "ReceptionAndTransmissions",
                newName: "IX_ReceptionAndTransmissions_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_DisposalInformation_TechnicalEquipmentId",
                table: "DisposalInformations",
                newName: "IX_DisposalInformations_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Conservation_TechnicalEquipmentId",
                table: "Conservations",
                newName: "IX_Conservations_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Conservation_EmployeeId",
                table: "Conservations",
                newName: "IX_Conservations_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedWork_TechnicalEquipmentId",
                table: "CompletedWorks",
                newName: "IX_CompletedWorks_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedWork_ApplicationsId",
                table: "CompletedWorks",
                newName: "IX_CompletedWorks_ApplicationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeEquipments",
                table: "TypeEquipments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storages",
                table: "Storages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceptionAndTransmissions",
                table: "ReceptionAndTransmissions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employees",
                table: "Employees",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisposalInformations",
                table: "DisposalInformations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conservations",
                table: "Conservations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedWorks",
                table: "CompletedWorks",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Locations_LocationId",
                table: "Applications",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWorks_Applications_ApplicationsId",
                table: "CompletedWorks",
                column: "ApplicationsId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWorks_TechnicalEquipment_TechnicalEquipmentId",
                table: "CompletedWorks",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conservations_Employees_EmployeeId",
                table: "Conservations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conservations_TechnicalEquipment_TechnicalEquipmentId",
                table: "Conservations",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DisposalInformations_TechnicalEquipment_TechnicalEquipmentId",
                table: "DisposalInformations",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moving_Employees_ConveyedId",
                table: "Moving",
                column: "ConveyedId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Moving_Employees_NewRespId",
                table: "Moving",
                column: "NewRespId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReceptionAndTransmissions_TechnicalEquipment_TechnicalEquip~",
                table: "ReceptionAndTransmissions",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Repairs_TechnicalEquipment_TechnicalEquipmentId",
                table: "Repairs",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Storages_TechnicalEquipment_TechnicalEquipmentId",
                table: "Storages",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_Employees_EmployeeId",
                table: "TechnicalEquipment",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_Locations_LocationId",
                table: "TechnicalEquipment",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TechnicalEquipment_TypeEquipments_TypeId",
                table: "TechnicalEquipment",
                column: "TypeId",
                principalTable: "TypeEquipments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Applications_Locations_LocationId",
                table: "Applications");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWorks_Applications_ApplicationsId",
                table: "CompletedWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_CompletedWorks_TechnicalEquipment_TechnicalEquipmentId",
                table: "CompletedWorks");

            migrationBuilder.DropForeignKey(
                name: "FK_Conservations_Employees_EmployeeId",
                table: "Conservations");

            migrationBuilder.DropForeignKey(
                name: "FK_Conservations_TechnicalEquipment_TechnicalEquipmentId",
                table: "Conservations");

            migrationBuilder.DropForeignKey(
                name: "FK_DisposalInformations_TechnicalEquipment_TechnicalEquipmentId",
                table: "DisposalInformations");

            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Employees_ConveyedId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_Moving_Employees_NewRespId",
                table: "Moving");

            migrationBuilder.DropForeignKey(
                name: "FK_ReceptionAndTransmissions_TechnicalEquipment_TechnicalEquip~",
                table: "ReceptionAndTransmissions");

            migrationBuilder.DropForeignKey(
                name: "FK_Repairs_TechnicalEquipment_TechnicalEquipmentId",
                table: "Repairs");

            migrationBuilder.DropForeignKey(
                name: "FK_Storages_TechnicalEquipment_TechnicalEquipmentId",
                table: "Storages");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Employees_EmployeeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_Locations_LocationId",
                table: "TechnicalEquipment");

            migrationBuilder.DropForeignKey(
                name: "FK_TechnicalEquipment_TypeEquipments_TypeId",
                table: "TechnicalEquipment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TypeEquipments",
                table: "TypeEquipments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Storages",
                table: "Storages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Repairs",
                table: "Repairs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReceptionAndTransmissions",
                table: "ReceptionAndTransmissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Employees",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DisposalInformations",
                table: "DisposalInformations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Conservations",
                table: "Conservations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompletedWorks",
                table: "CompletedWorks");

            migrationBuilder.RenameTable(
                name: "TypeEquipments",
                newName: "TypeEquipment");

            migrationBuilder.RenameTable(
                name: "Storages",
                newName: "Storage");

            migrationBuilder.RenameTable(
                name: "Repairs",
                newName: "Repair");

            migrationBuilder.RenameTable(
                name: "ReceptionAndTransmissions",
                newName: "ReceptionAndTransmission");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "Location");

            migrationBuilder.RenameTable(
                name: "Employees",
                newName: "Employee");

            migrationBuilder.RenameTable(
                name: "DisposalInformations",
                newName: "DisposalInformation");

            migrationBuilder.RenameTable(
                name: "Conservations",
                newName: "Conservation");

            migrationBuilder.RenameTable(
                name: "CompletedWorks",
                newName: "CompletedWork");

            migrationBuilder.RenameIndex(
                name: "IX_Storages_TechnicalEquipmentId",
                table: "Storage",
                newName: "IX_Storage_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Repairs_TechnicalEquipmentId",
                table: "Repair",
                newName: "IX_Repair_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_ReceptionAndTransmissions_TechnicalEquipmentId",
                table: "ReceptionAndTransmission",
                newName: "IX_ReceptionAndTransmission_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_DisposalInformations_TechnicalEquipmentId",
                table: "DisposalInformation",
                newName: "IX_DisposalInformation_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Conservations_TechnicalEquipmentId",
                table: "Conservation",
                newName: "IX_Conservation_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_Conservations_EmployeeId",
                table: "Conservation",
                newName: "IX_Conservation_EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedWorks_TechnicalEquipmentId",
                table: "CompletedWork",
                newName: "IX_CompletedWork_TechnicalEquipmentId");

            migrationBuilder.RenameIndex(
                name: "IX_CompletedWorks_ApplicationsId",
                table: "CompletedWork",
                newName: "IX_CompletedWork_ApplicationsId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TypeEquipment",
                table: "TypeEquipment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Storage",
                table: "Storage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Repair",
                table: "Repair",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReceptionAndTransmission",
                table: "ReceptionAndTransmission",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Location",
                table: "Location",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DisposalInformation",
                table: "DisposalInformation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Conservation",
                table: "Conservation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompletedWork",
                table: "CompletedWork",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Applications_Location_LocationId",
                table: "Applications",
                column: "LocationId",
                principalTable: "Location",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWork_Applications_ApplicationsId",
                table: "CompletedWork",
                column: "ApplicationsId",
                principalTable: "Applications",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompletedWork_TechnicalEquipment_TechnicalEquipmentId",
                table: "CompletedWork",
                column: "TechnicalEquipmentId",
                principalTable: "TechnicalEquipment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conservation_Employee_EmployeeId",
                table: "Conservation",
                column: "EmployeeId",
                principalTable: "Employee",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Conservation_TechnicalEquipment_TechnicalEquipmentId",
                table: "Conservation",
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
    }
}

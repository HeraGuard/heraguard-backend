using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace heraguard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixesAndRenames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctores_Users_UserId",
                table: "Doctores");

            migrationBuilder.DropTable(
                name: "AdultosMayores");

            migrationBuilder.DropTable(
                name: "Familiares");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctores",
                table: "Doctores");

            migrationBuilder.RenameTable(
                name: "Doctores",
                newName: "Doctors");

            migrationBuilder.RenameColumn(
                name: "Especialidad",
                table: "Doctors",
                newName: "Specialty");

            migrationBuilder.RenameColumn(
                name: "CentroMedico",
                table: "Doctors",
                newName: "MedicalLicense");

            migrationBuilder.RenameColumn(
                name: "Cedula",
                table: "Doctors",
                newName: "MedicalCenter");

            migrationBuilder.AddColumn<Guid>(
                name: "CaregiverId",
                table: "Medications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "DoctorId",
                table: "Medications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ElderId",
                table: "Medications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "Caregivers",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Relationship = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Caregivers", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Caregivers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Elders",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EmergencyContact = table.Column<string>(type: "text", nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Elders", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Elders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Medications_CaregiverId",
                table: "Medications",
                column: "CaregiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_DoctorId",
                table: "Medications",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_ElderId",
                table: "Medications",
                column: "ElderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Caregivers_CaregiverId",
                table: "Medications",
                column: "CaregiverId",
                principalTable: "Caregivers",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Doctors_DoctorId",
                table: "Medications",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_Elders_ElderId",
                table: "Medications",
                column: "ElderId",
                principalTable: "Elders",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_Users_UserId",
                table: "Doctors");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Caregivers_CaregiverId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Doctors_DoctorId",
                table: "Medications");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_Elders_ElderId",
                table: "Medications");

            migrationBuilder.DropTable(
                name: "Caregivers");

            migrationBuilder.DropTable(
                name: "Elders");

            migrationBuilder.DropIndex(
                name: "IX_Medications_CaregiverId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_DoctorId",
                table: "Medications");

            migrationBuilder.DropIndex(
                name: "IX_Medications_ElderId",
                table: "Medications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Doctors",
                table: "Doctors");

            migrationBuilder.DropColumn(
                name: "CaregiverId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Medications");

            migrationBuilder.DropColumn(
                name: "ElderId",
                table: "Medications");

            migrationBuilder.RenameTable(
                name: "Doctors",
                newName: "Doctores");

            migrationBuilder.RenameColumn(
                name: "Specialty",
                table: "Doctores",
                newName: "Especialidad");

            migrationBuilder.RenameColumn(
                name: "MedicalLicense",
                table: "Doctores",
                newName: "CentroMedico");

            migrationBuilder.RenameColumn(
                name: "MedicalCenter",
                table: "Doctores",
                newName: "Cedula");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Doctores",
                table: "Doctores",
                column: "UserId");

            migrationBuilder.CreateTable(
                name: "AdultosMayores",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ContactoEmergencia = table.Column<string>(type: "text", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdultosMayores", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_AdultosMayores_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Familiares",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    Direccion = table.Column<string>(type: "text", nullable: false),
                    Parentesco = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Familiares", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Familiares_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Doctores_Users_UserId",
                table: "Doctores",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

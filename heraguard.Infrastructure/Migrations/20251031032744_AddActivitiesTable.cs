using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace heraguard.Infrastructure.Migrations
{
    public partial class AddActivitiesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Frequency = table.Column<string>(type: "text", nullable: false),
                    RecommendedTime = table.Column<TimeSpan>(type: "interval", nullable: false),
                    Duration = table.Column<string>(type: "text", nullable: false),
                    Notes = table.Column<string>(type: "text", nullable: true),
                    DoctorId = table.Column<Guid>(type: "uuid", nullable: true),
                    CaregiverId = table.Column<Guid>(type: "uuid", nullable: true),
                    ElderId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityId);
                    table.ForeignKey(
                        name: "FK_Activities_Caregivers_CaregiverId",
                        column: x => x.CaregiverId,
                        principalTable: "Caregivers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Activities_Elders_ElderId",
                        column: x => x.ElderId,
                        principalTable: "Elders",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Activities_CaregiverId",
                table: "Activities",
                column: "CaregiverId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_DoctorId",
                table: "Activities",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_Activities_ElderId",
                table: "Activities",
                column: "ElderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Activities");
        }
    }
}
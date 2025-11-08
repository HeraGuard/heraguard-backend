using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace heraguard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Relationships",
                columns: table => new
                {
                    RelationshipId = table.Column<Guid>(type: "uuid", nullable: false),
                    ElderId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelatedUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RelationshipTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Relationships", x => x.RelationshipId);
                    table.ForeignKey(
                        name: "FK_Relationships_Users_ElderId",
                        column: x => x.ElderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Relationships_Users_RelatedUserId",
                        column: x => x.RelatedUserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_ElderId",
                table: "Relationships",
                column: "ElderId");

            migrationBuilder.CreateIndex(
                name: "IX_Relationships_RelatedUserId",
                table: "Relationships",
                column: "RelatedUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Relationships");
        }
    }
}

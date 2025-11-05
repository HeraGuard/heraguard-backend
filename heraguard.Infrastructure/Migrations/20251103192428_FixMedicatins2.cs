using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace heraguard.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixMedicatins2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*
            migrationBuilder.AlterColumn<int>(
                name: "Frequency",
                table: "Medications",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
            */
            migrationBuilder.Sql(
                "ALTER TABLE \"Medications\" ALTER COLUMN \"Frequency\" TYPE integer USING \"Frequency\"::integer;"
            );

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Medications",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Medications");

            migrationBuilder.AlterColumn<string>(
                name: "Frequency",
                table: "Medications",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerManagement.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class ServerSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OperatingSystem",
                schema: "ServerMgmt",
                table: "Servers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OperatingSystem",
                schema: "ServerMgmt",
                table: "Servers");
        }
    }
}

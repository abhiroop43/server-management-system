using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerManagement.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class UpdateServerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GeographicRegion",
                schema: "ServerMgmt",
                table: "Servers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: ""
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GeographicRegion",
                schema: "ServerMgmt",
                table: "Servers"
            );
        }
    }
}

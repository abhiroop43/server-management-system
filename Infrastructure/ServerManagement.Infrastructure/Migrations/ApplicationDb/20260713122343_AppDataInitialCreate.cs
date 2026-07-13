using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerManagement.Infrastructure.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AppDataInitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(name: "ServerMgmt");

            migrationBuilder.CreateTable(
                name: "Servers",
                schema: "ServerMgmt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsOnline = table.Column<bool>(type: "bit", nullable: false),
                    Status = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false,
                        defaultValue: "Running"
                    ),
                    IpAddresses = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CpuCores = table.Column<int>(type: "int", nullable: false),
                    MemoryInGb = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UpTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    LastSeen = table.Column<DateTimeOffset>(
                        type: "datetimeoffset",
                        nullable: false
                    ),
                    DecommissionedAt = table.Column<DateTimeOffset>(
                        type: "datetimeoffset",
                        nullable: true
                    ),
                    HealthScore = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Tags = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Metadata = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    OwnerId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HostName = table.Column<string>(
                        type: "nvarchar(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    Name = table.Column<string>(
                        type: "nvarchar(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    PrimaryIpAddress = table.Column<string>(
                        type: "nvarchar(15)",
                        maxLength: 15,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servers", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "Disks",
                schema: "ServerMgmt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CapacityGb = table.Column<long>(type: "bigint", nullable: false),
                    UsedGb = table.Column<long>(type: "bigint", nullable: false),
                    DiskType = table.Column<string>(
                        type: "nvarchar(max)",
                        nullable: false,
                        defaultValue: "SSD"
                    ),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(
                        type: "nvarchar(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Disks_Servers_ServerId",
                        column: x => x.ServerId,
                        principalSchema: "ServerMgmt",
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateTable(
                name: "HostedServices",
                schema: "ServerMgmt",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Port = table.Column<int>(type: "int", nullable: false),
                    IsListening = table.Column<bool>(type: "bit", nullable: false),
                    LastChecked = table.Column<DateTimeOffset>(
                        type: "datetimeoffset",
                        nullable: false
                    ),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    HostedServiceName = table.Column<string>(
                        type: "nvarchar(128)",
                        maxLength: 128,
                        nullable: false
                    ),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HostedServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HostedServices_Servers_ServerId",
                        column: x => x.ServerId,
                        principalSchema: "ServerMgmt",
                        principalTable: "Servers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_Disks_ServerId",
                schema: "ServerMgmt",
                table: "Disks",
                column: "ServerId"
            );

            migrationBuilder.CreateIndex(
                name: "IX_HostedServices_ServerId",
                schema: "ServerMgmt",
                table: "HostedServices",
                column: "ServerId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "Disks", schema: "ServerMgmt");

            migrationBuilder.DropTable(name: "HostedServices", schema: "ServerMgmt");

            migrationBuilder.DropTable(name: "Servers", schema: "ServerMgmt");
        }
    }
}

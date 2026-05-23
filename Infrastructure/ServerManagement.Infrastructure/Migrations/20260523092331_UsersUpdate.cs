using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UsersUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: ""
            );

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "DateOfBirth", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "Discriminator", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "FirstName", table: "AspNetUsers");

            migrationBuilder.DropColumn(name: "LastName", table: "AspNetUsers");
        }
    }
}

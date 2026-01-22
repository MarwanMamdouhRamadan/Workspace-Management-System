using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureAspNetIdentit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "TbInvoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "TbInvoice",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdtedBy",
                table: "TbInvoice",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbInvoice",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "TbBooking",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbInvoice_UserId",
                table: "TbInvoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbBooking_UserId",
                table: "TbBooking",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_TbBooking_ApplicationUser_UserId",
                table: "TbBooking",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TbInvoice_ApplicationUser_UserId",
                table: "TbInvoice",
                column: "UserId",
                principalTable: "ApplicationUser",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbBooking_ApplicationUser_UserId",
                table: "TbBooking");

            migrationBuilder.DropForeignKey(
                name: "FK_TbInvoice_ApplicationUser_UserId",
                table: "TbInvoice");

            migrationBuilder.DropTable(
                name: "ApplicationUser");

            migrationBuilder.DropIndex(
                name: "IX_TbInvoice_UserId",
                table: "TbInvoice");

            migrationBuilder.DropIndex(
                name: "IX_TbBooking_UserId",
                table: "TbBooking");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "TbInvoice");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "TbInvoice");

            migrationBuilder.DropColumn(
                name: "UpdtedBy",
                table: "TbInvoice");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbInvoice");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "TbBooking");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddStatusToRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "TbRoom",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TbRoom_StatusId",
                table: "TbRoom",
                column: "StatusId",
                unique: false);

            migrationBuilder.AddForeignKey(
                name: "FK_TbRoom_TbStatus_StatusId",
                table: "TbRoom",
                column: "StatusId",
                principalTable: "TbStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbRoom_TbStatus_StatusId",
                table: "TbRoom");

            migrationBuilder.DropIndex(
                name: "IX_TbRoom_StatusId",
                table: "TbRoom");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TbRoom");
        }
    }
}

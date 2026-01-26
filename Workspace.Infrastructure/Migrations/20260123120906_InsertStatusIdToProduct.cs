using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InsertStatusIdToProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "StatusId",
                table: "TbProduct",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_TbProduct_StatusId",
                table: "TbProduct",
                column: "StatusId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TbProduct_TbStatus_StatusId",
                table: "TbProduct",
                column: "StatusId",
                principalTable: "TbStatus",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TbProduct_TbStatus_StatusId",
                table: "TbProduct");

            migrationBuilder.DropIndex(
                name: "IX_TbProduct_StatusId",
                table: "TbProduct");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "TbProduct");
        }
    }
}

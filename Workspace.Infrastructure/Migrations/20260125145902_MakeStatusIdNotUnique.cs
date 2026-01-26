using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MakeStatusIdNotUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbProduct_StatusId",
                table: "TbProduct");

            migrationBuilder.CreateIndex(
                name: "IX_TbProduct_StatusId",
                table: "TbProduct",
                column: "StatusId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_TbProduct_StatusId",
                table: "TbProduct");

            migrationBuilder.CreateIndex(
                name: "IX_TbProduct_StatusId",
                table: "TbProduct",
                column: "StatusId",
                unique: true);
        }
    }
}

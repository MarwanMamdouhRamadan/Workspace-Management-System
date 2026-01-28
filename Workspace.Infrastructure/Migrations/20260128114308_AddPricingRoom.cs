using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPricingRoom : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TbRoomPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    PricingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRoomPricing", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbRoomPricing_TbPricingType_PricingTypeId",
                        column: x => x.PricingTypeId,
                        principalTable: "TbPricingType",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TbRoomPricing_TbRoom_RoomId",
                        column: x => x.RoomId,
                        principalTable: "TbRoom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbRoomPricing_PricingTypeId",
                table: "TbRoomPricing",
                column: "PricingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRoomPricing_RoomId",
                table: "TbRoomPricing",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbRoomPricing");
        }
    }
}

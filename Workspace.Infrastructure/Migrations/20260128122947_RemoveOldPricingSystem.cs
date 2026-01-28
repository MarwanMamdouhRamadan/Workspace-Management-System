using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOldPricingSystem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Room_PricingType",
                table: "TbRoom");

            migrationBuilder.DropTable(
                name: "TbRoomPricing");

            migrationBuilder.DropTable(
                name: "TbPricingType");

            migrationBuilder.DropIndex(
                name: "IX_TbRoom_PricingTypeID",
                table: "TbRoom");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "TbRoom");

            migrationBuilder.DropColumn(
                name: "PricingTypeID",
                table: "TbRoom");

            migrationBuilder.CreateTable(
                name: "TbRoomRates",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
                    Mode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HourlyRate = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TbRoomRates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TbRoomRates_TbRoom_RoomId",
                        column: x => x.RoomId,
                        principalTable: "TbRoom",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TbRoomRates_RoomId",
                table: "TbRoomRates",
                column: "RoomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TbRoomRates");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "TbRoom",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "PricingTypeID",
                table: "TbRoom",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "TbPricingType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbPricin__3214EC27D0AA45A7", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TbRoomPricing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PricingTypeId = table.Column<long>(type: "bigint", nullable: false),
                    RoomId = table.Column<long>(type: "bigint", nullable: false),
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
                name: "IX_TbRoom_PricingTypeID",
                table: "TbRoom",
                column: "PricingTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_TbRoomPricing_PricingTypeId",
                table: "TbRoomPricing",
                column: "PricingTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_TbRoomPricing_RoomId",
                table: "TbRoomPricing",
                column: "RoomId");

            migrationBuilder.AddForeignKey(
                name: "FK_Room_PricingType",
                table: "TbRoom",
                column: "PricingTypeID",
                principalTable: "TbPricingType",
                principalColumn: "ID");
        }
    }
}

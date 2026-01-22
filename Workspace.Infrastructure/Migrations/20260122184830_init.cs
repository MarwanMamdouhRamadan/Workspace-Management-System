using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Workspace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
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
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TbPricingType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbPricin__3214EC27D0AA45A7", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TbProduct",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbProduc__3214EC27683B6026", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TbSetting",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KeyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    KeyValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbSettin__3214EC2784368E9B", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "TbStatusType",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbStatus__3214EC27AFD78D69", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TbRoom",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    PricingTypeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbRoom__3214EC27F54565C8", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Room_PricingType",
                        column: x => x.PricingTypeID,
                        principalTable: "TbPricingType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TbStatus",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatusName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StatusTypeID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbStatus__3214EC272F2BE8D6", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Status_StatusType",
                        column: x => x.StatusTypeID,
                        principalTable: "TbStatusType",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TbMedia",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RelativePathFile = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    RoomID = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbMedia__3214EC2720E6FCBA", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Media_Room",
                        column: x => x.RoomID,
                        principalTable: "TbRoom",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TbBooking",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomID = table.Column<long>(type: "bigint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    RoomPrice = table.Column<decimal>(type: "money", nullable: false),
                    StatusID = table.Column<long>(type: "bigint", nullable: false),
                    CountOfPeople = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbBookin__3214EC27BEC47BF2", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Booking_Room",
                        column: x => x.RoomID,
                        principalTable: "TbRoom",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Booking_Status",
                        column: x => x.StatusID,
                        principalTable: "TbStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TbBooking_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbInvoice",
                columns: table => new
                {
                    ID = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InvoiceNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdtedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InvoiceDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    TotalRoomPrice = table.Column<decimal>(type: "money", nullable: false),
                    TotalProductPrice = table.Column<decimal>(type: "money", nullable: false),
                    SubTotal = table.Column<decimal>(type: "money", nullable: false),
                    DiscountPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    DiscountAmount = table.Column<decimal>(type: "money", nullable: false),
                    TotalAfterDiscount = table.Column<decimal>(type: "money", nullable: false),
                    TaxPercentage = table.Column<decimal>(type: "decimal(5,2)", nullable: false),
                    TaxAmount = table.Column<decimal>(type: "money", nullable: false),
                    GrandTotal = table.Column<decimal>(type: "money", nullable: false),
                    StatusID = table.Column<long>(type: "bigint", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__TbInvoic__3214EC27D1866D83", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Invoice_Status",
                        column: x => x.StatusID,
                        principalTable: "TbStatus",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_TbInvoice_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TbBookingProduct",
                columns: table => new
                {
                    BookingID = table.Column<long>(type: "bigint", nullable: false),
                    ProductID = table.Column<long>(type: "bigint", nullable: false),
                    ProductQty = table.Column<int>(type: "int", nullable: false),
                    ProductUnitPrice = table.Column<decimal>(type: "money", nullable: false),
                    ProductTotalPrice = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingProduct", x => new { x.BookingID, x.ProductID });
                    table.ForeignKey(
                        name: "FK_BookingProduct_Booking",
                        column: x => x.BookingID,
                        principalTable: "TbBooking",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BookingProduct_Product",
                        column: x => x.ProductID,
                        principalTable: "TbProduct",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "TbInvoiceBooking",
                columns: table => new
                {
                    InvoiceID = table.Column<long>(type: "bigint", nullable: false),
                    BookingID = table.Column<long>(type: "bigint", nullable: false),
                    BookingRoomPrice = table.Column<decimal>(type: "money", nullable: false),
                    BookingProductPrice = table.Column<decimal>(type: "money", nullable: false),
                    BookingTotal = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvoiceBooking", x => new { x.InvoiceID, x.BookingID });
                    table.ForeignKey(
                        name: "FK_InvoiceBooking_Booking",
                        column: x => x.BookingID,
                        principalTable: "TbBooking",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_InvoiceBooking_Invoice",
                        column: x => x.InvoiceID,
                        principalTable: "TbInvoice",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_TbBooking_RoomID",
                table: "TbBooking",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_TbBooking_StatusID",
                table: "TbBooking",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TbBooking_UserId",
                table: "TbBooking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TbBookingProduct_ProductID",
                table: "TbBookingProduct",
                column: "ProductID");

            migrationBuilder.CreateIndex(
                name: "IX_TbInvoice_StatusID",
                table: "TbInvoice",
                column: "StatusID");

            migrationBuilder.CreateIndex(
                name: "IX_TbInvoice_UserId",
                table: "TbInvoice",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "UQ__TbInvoic__D776E9816693A655",
                table: "TbInvoice",
                column: "InvoiceNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UQ_InvoiceBooking_BookingID",
                table: "TbInvoiceBooking",
                column: "BookingID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbMedia_RoomID",
                table: "TbMedia",
                column: "RoomID");

            migrationBuilder.CreateIndex(
                name: "IX_TbRoom_PricingTypeID",
                table: "TbRoom",
                column: "PricingTypeID");

            migrationBuilder.CreateIndex(
                name: "UQ__TbSettin__F0A2A3372F699BDC",
                table: "TbSetting",
                column: "KeyName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TbStatus_StatusTypeID",
                table: "TbStatus",
                column: "StatusTypeID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TbBookingProduct");

            migrationBuilder.DropTable(
                name: "TbInvoiceBooking");

            migrationBuilder.DropTable(
                name: "TbMedia");

            migrationBuilder.DropTable(
                name: "TbSetting");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "TbProduct");

            migrationBuilder.DropTable(
                name: "TbBooking");

            migrationBuilder.DropTable(
                name: "TbInvoice");

            migrationBuilder.DropTable(
                name: "TbRoom");

            migrationBuilder.DropTable(
                name: "TbStatus");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TbPricingType");

            migrationBuilder.DropTable(
                name: "TbStatusType");
        }
    }
}

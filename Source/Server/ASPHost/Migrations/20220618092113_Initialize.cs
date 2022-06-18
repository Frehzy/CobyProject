using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASPHost.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllDiscounts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    DiscountSum = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllDiscounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllProducts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    Type = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllProducts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllTables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllTables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllWaiters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Permissions = table.Column<byte[]>(type: "smallint[]", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllWaiters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GuestEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GuestEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTypeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Kind = table.Column<int>(type: "integer", nullable: false),
                    NeedOpenCashBox = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypeEntity", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderWaiterEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderWaiterEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderWaiterEntity_AllWaiters_Id",
                        column: x => x.Id,
                        principalTable: "AllWaiters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPaymentTypeEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentTypeEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPaymentTypeEntity_PaymentTypeEntity_Id",
                        column: x => x.Id,
                        principalTable: "PaymentTypeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AllOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
                    WaiterId = table.Column<Guid>(type: "uuid", nullable: false),
                    TablesId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    GuestsId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    ProductsId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    DiscountsId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    PaymentsId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CloseTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllOrders_OrderWaiterEntity_WaiterId",
                        column: x => x.WaiterId,
                        principalTable: "OrderWaiterEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDiscountEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDiscountEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDiscountEntity_AllDiscounts_Id",
                        column: x => x.Id,
                        principalTable: "AllDiscounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDiscountEntity_AllOrders_DiscountsId",
                        column: x => x.DiscountsId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGuestEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGuestEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGuestEntity_AllOrders_GuestsId",
                        column: x => x.GuestsId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderGuestEntity_GuestEntity_Id",
                        column: x => x.Id,
                        principalTable: "GuestEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPaymentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    PaymentTypeId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentsId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPaymentEntity_AllOrders_PaymentsId",
                        column: x => x.PaymentsId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPaymentEntity_OrderPaymentTypeEntity_PaymentTypeId",
                        column: x => x.PaymentTypeId,
                        principalTable: "OrderPaymentTypeEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductsId = table.Column<Guid>(type: "uuid", nullable: false),
                    GuestId = table.Column<Guid>(type: "uuid", nullable: false),
                    WaiterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<byte>(type: "smallint", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductEntity_AllOrders_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderProductEntity_AllProducts_Id",
                        column: x => x.Id,
                        principalTable: "AllProducts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTableEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TablesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTableEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTableEntity_AllOrders_TablesId",
                        column: x => x.TablesId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderTableEntity_AllTables_Id",
                        column: x => x.Id,
                        principalTable: "AllTables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AllWaiters",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "Name", "Password", "Permissions", "UpdateTime", "Version", "WaiterCreatedId", "WaiterUpdatedId" },
                values: new object[] { new Guid("2c4c850c-728e-45a9-bb41-615f5723e0aa"), new DateTime(2022, 6, 18, 14, 20, 56, 117, DateTimeKind.Local).AddTicks(6), false, "ADMIN", "ADMINPASSWORD", new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 }, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_AllOrders_WaiterId",
                table: "AllOrders",
                column: "WaiterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderDiscountEntity_DiscountsId",
                table: "OrderDiscountEntity",
                column: "DiscountsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGuestEntity_GuestsId",
                table: "OrderGuestEntity",
                column: "GuestsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentEntity_PaymentsId",
                table: "OrderPaymentEntity",
                column: "PaymentsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentEntity_PaymentTypeId",
                table: "OrderPaymentEntity",
                column: "PaymentTypeId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductEntity_ProductsId",
                table: "OrderProductEntity",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTableEntity_TablesId",
                table: "OrderTableEntity",
                column: "TablesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDiscountEntity");

            migrationBuilder.DropTable(
                name: "OrderGuestEntity");

            migrationBuilder.DropTable(
                name: "OrderPaymentEntity");

            migrationBuilder.DropTable(
                name: "OrderProductEntity");

            migrationBuilder.DropTable(
                name: "OrderTableEntity");

            migrationBuilder.DropTable(
                name: "AllDiscounts");

            migrationBuilder.DropTable(
                name: "GuestEntity");

            migrationBuilder.DropTable(
                name: "OrderPaymentTypeEntity");

            migrationBuilder.DropTable(
                name: "AllProducts");

            migrationBuilder.DropTable(
                name: "AllOrders");

            migrationBuilder.DropTable(
                name: "AllTables");

            migrationBuilder.DropTable(
                name: "PaymentTypeEntity");

            migrationBuilder.DropTable(
                name: "OrderWaiterEntity");

            migrationBuilder.DropTable(
                name: "AllWaiters");
        }
    }
}

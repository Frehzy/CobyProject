using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiHostData.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllDiscountTypes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllDiscountTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllOrders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Number = table.Column<int>(type: "integer", nullable: false),
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
                });

            migrationBuilder.CreateTable(
                name: "AllPaymentTypes",
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
                    table.PrimaryKey("PK_AllPaymentTypes", x => x.Id);
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
                    Name = table.Column<string>(type: "text", nullable: false),
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
                    IsSessionOpen = table.Column<bool>(type: "boolean", nullable: false),
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
                name: "OrderDiscountEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountTypeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    DiscountSum = table.Column<decimal>(type: "numeric", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDiscountEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDiscountEntity_AllOrders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderGuestEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Rank = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderGuestEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderGuestEntity_AllOrders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderPaymentEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PaymentTypeEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    Sum = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPaymentEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPaymentEntity_AllOrders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderProductEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    ProductItemEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderGuestEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderWaiterEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    PrintTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<byte>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderProductEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderProductEntity_AllOrders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderTableEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    TableEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderTableEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderTableEntity_AllOrders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderWaiterEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrderEntityId = table.Column<Guid>(type: "uuid", nullable: false),
                    WaiterEntityId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderWaiterEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderWaiterEntity_AllOrders_OrderEntityId",
                        column: x => x.OrderEntityId,
                        principalTable: "AllOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AllWaiters",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "IsSessionOpen", "Name", "Password", "Permissions", "UpdateTime", "Version", "WaiterCreatedId", "WaiterUpdatedId" },
                values: new object[] { new Guid("2c4c850c-728e-45a9-bb41-615f5723e0aa"), new DateTime(2022, 6, 25, 18, 54, 10, 118, DateTimeKind.Local).AddTicks(4290), false, false, "ADMIN", "ADMINPASSWORD", new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 }, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDiscountEntity_OrderEntityId",
                table: "OrderDiscountEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderGuestEntity_OrderEntityId",
                table: "OrderGuestEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderPaymentEntity_OrderEntityId",
                table: "OrderPaymentEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderProductEntity_OrderEntityId",
                table: "OrderProductEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderTableEntity_OrderEntityId",
                table: "OrderTableEntity",
                column: "OrderEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderWaiterEntity_OrderEntityId",
                table: "OrderWaiterEntity",
                column: "OrderEntityId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllDiscountTypes");

            migrationBuilder.DropTable(
                name: "AllPaymentTypes");

            migrationBuilder.DropTable(
                name: "AllProducts");

            migrationBuilder.DropTable(
                name: "AllTables");

            migrationBuilder.DropTable(
                name: "AllWaiters");

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
                name: "OrderWaiterEntity");

            migrationBuilder.DropTable(
                name: "AllOrders");
        }
    }
}

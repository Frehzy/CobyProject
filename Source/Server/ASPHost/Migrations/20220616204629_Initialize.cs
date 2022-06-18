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
                name: "AllWaiterPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionsId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllWaiterPermissions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AllWaiters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
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
                name: "AllPermissions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    EmployeePermission = table.Column<byte>(type: "smallint", nullable: false),
                    PermissionsId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllPermissions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AllPermissions_AllWaiterPermissions_PermissionsId",
                        column: x => x.PermissionsId,
                        principalTable: "AllWaiterPermissions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "OrderWaiterEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<List<Guid>>(type: "uuid[]", nullable: false),
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
                name: "OrderPermissionEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    PermissionId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderPermissionEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderPermissionEntity_AllPermissions_Id",
                        column: x => x.Id,
                        principalTable: "AllPermissions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderPermissionEntity_OrderWaiterEntity_PermissionId",
                        column: x => x.PermissionId,
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
                table: "AllPermissions",
                columns: new[] { "Id", "CreatedTime", "EmployeePermission", "IsDeleted", "PermissionsId", "UpdateTime", "Version", "WaiterCreatedId", "WaiterUpdatedId" },
                values: new object[,]
                {
                    { new Guid("0188f550-96ed-418c-b330-1e7af3e550b6"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3968), (byte)16, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("058db51d-b54a-4b18-8c25-8ff7f12e0aa0"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3914), (byte)8, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("17402627-055f-4a02-ac5b-b8f34e8898bb"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(4041), (byte)21, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("27aa8fa8-db9a-456c-9517-63966b832d78"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3910), (byte)6, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("31579291-81b3-4fa6-8e5b-e804695c00d6"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(4037), (byte)19, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("368558a3-e579-457c-b9fa-35618cfd34ea"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3912), (byte)7, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("4921886e-0711-4179-bb86-dee55b8a6817"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3945), (byte)13, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("495f09fd-dfe2-45a0-b042-5731aede5c68"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3942), (byte)12, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("55684f46-0dbb-48c9-b4b3-d927d53c7b3b"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3908), (byte)5, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("5b537529-6e0c-4127-8e15-6172745d94be"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3920), (byte)10, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("666c73da-ba0e-4a74-88ad-7fb3154d6acc"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3902), (byte)4, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("7c2ea4b6-8cd0-4ee2-8d22-ac3932ea905b"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3897), (byte)2, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("8a8ce162-257e-4f69-8aa9-b517404726e4"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3894), (byte)1, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("9a7c7a24-5660-4237-bb95-a34687c33c1b"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(4039), (byte)20, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a1460813-bfe1-4c7f-b658-62b5083f9e06"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3931), (byte)11, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a704fcde-ba5d-42be-89c8-f1e64ec8a522"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3966), (byte)15, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("a93b13ab-a858-4d96-889d-4bfffe2fbfbc"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(4035), (byte)18, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("c689b455-0f8a-414b-8a69-64b165826792"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3963), (byte)14, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("d90fb9f8-6e90-4de5-b239-23f45468931c"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(4032), (byte)17, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e256dce3-19e8-43f8-995f-5a7aa821c946"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3862), (byte)0, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("e5fdef70-3f4c-48ed-b042-953727a84caf"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3918), (byte)9, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") },
                    { new Guid("f578dc7d-4344-4d90-985a-d4b9eaca3661"), new DateTime(2022, 6, 17, 1, 46, 19, 530, DateTimeKind.Local).AddTicks(3900), (byte)3, false, null, null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllOrders_WaiterId",
                table: "AllOrders",
                column: "WaiterId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AllPermissions_PermissionsId",
                table: "AllPermissions",
                column: "PermissionsId");

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
                name: "IX_OrderPermissionEntity_PermissionId",
                table: "OrderPermissionEntity",
                column: "PermissionId");

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
                name: "OrderPermissionEntity");

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
                name: "AllPermissions");

            migrationBuilder.DropTable(
                name: "AllProducts");

            migrationBuilder.DropTable(
                name: "AllOrders");

            migrationBuilder.DropTable(
                name: "AllTables");

            migrationBuilder.DropTable(
                name: "PaymentTypeEntity");

            migrationBuilder.DropTable(
                name: "AllWaiterPermissions");

            migrationBuilder.DropTable(
                name: "OrderWaiterEntity");

            migrationBuilder.DropTable(
                name: "AllWaiters");
        }
    }
}

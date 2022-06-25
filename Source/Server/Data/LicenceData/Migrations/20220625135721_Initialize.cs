using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LicenceData.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AllLicences",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    OrganizationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ModuleLicenceId = table.Column<int>(type: "integer", nullable: false),
                    MaxReservedLicence = table.Column<int>(type: "integer", nullable: false),
                    CreatedTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    WaiterCreatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    UpdateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    WaiterUpdatedId = table.Column<Guid>(type: "uuid", nullable: false),
                    Version = table.Column<int>(type: "integer", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllLicences", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AllLicences",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "MaxReservedLicence", "ModuleLicenceId", "OrganizationId", "UpdateTime", "Version", "WaiterCreatedId", "WaiterUpdatedId" },
                values: new object[] { new Guid("2f5264c6-a805-47ed-9052-b9dc461df8db"), new DateTime(2022, 6, 25, 18, 57, 8, 960, DateTimeKind.Local).AddTicks(9620), false, 10, 5050, new Guid("00000000-0000-0000-0000-000000000000"), null, 1, new Guid("00000000-0000-0000-0000-000000000000"), new Guid("00000000-0000-0000-0000-000000000000") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllLicences");
        }
    }
}

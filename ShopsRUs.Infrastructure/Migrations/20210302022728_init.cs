using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShopsRUs.Infrastructure.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Discounts",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    DiscountType = table.Column<string>(type: "TEXT", nullable: true),
                    Value = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Discounts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Gender = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    UserType = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    PhoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    UpdatedOn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalAMountSpent = table.Column<decimal>(type: "TEXT", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    LastVisited = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Item = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    TotalCost = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalAMountPaid = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiscountedAmount = table.Column<decimal>(type: "TEXT", nullable: false),
                    DiscountId = table.Column<long>(type: "INTEGER", nullable: false),
                    UserId = table.Column<long>(type: "INTEGER", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Invoices_Discounts_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoices_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedOn", "DiscountType", "Name", "UpdatedOn", "Value" },
                values: new object[] { 1L, new DateTime(2021, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(5658), "Percentage", "Affiliate", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "10" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedOn", "DiscountType", "Name", "UpdatedOn", "Value" },
                values: new object[] { 2L, new DateTime(2021, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(6178), "Percentage", "Employee", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "30" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedOn", "DiscountType", "Name", "UpdatedOn", "Value" },
                values: new object[] { 3L, new DateTime(2021, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(6185), "Flat", "Customer", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5" });

            migrationBuilder.InsertData(
                table: "Discounts",
                columns: new[] { "Id", "CreatedOn", "DiscountType", "Name", "UpdatedOn", "Value" },
                values: new object[] { 4L, new DateTime(2021, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(6188), "Flat", "Default", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "5" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOn", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "PhoneNumber", "UpdatedOn", "UserType" },
                values: new object[] { 1L, "kfc road 1", new DateTime(2021, 3, 2, 3, 27, 27, 797, DateTimeKind.Local).AddTicks(8668), new DateTime(1971, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(965), "mckensie1@gmail.com", "male", true, "William 1", "08109502101", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Affiliate" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOn", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "PhoneNumber", "UpdatedOn", "UserType" },
                values: new object[] { 2L, "kfc road 2", new DateTime(2021, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(2711), new DateTime(1981, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(2728), "mckensie2@gmail.com", "male", true, "William 2", "08109502101", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOn", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "PhoneNumber", "UpdatedOn", "UserType" },
                values: new object[] { 3L, "kfc road 3", new DateTime(2018, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1991, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(2743), "mckensie3@gmail.com", "male", true, "William 3", "08109502103", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Employee" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOn", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "PhoneNumber", "UpdatedOn", "UserType" },
                values: new object[] { 4L, "kfc road 4", new DateTime(2018, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1987, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(2748), "mckensie4@gmail.com", "female", true, "William 4", "08109502104", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CreatedOn", "DateOfBirth", "Email", "Gender", "IsActive", "Name", "PhoneNumber", "UpdatedOn", "UserType" },
                values: new object[] { 5L, "kfc road 4", new DateTime(2020, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1987, 3, 2, 3, 27, 27, 798, DateTimeKind.Local).AddTicks(2751), "mckensie5@gmail.com", "female", true, "William 5", "08109502105", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Customer" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedOn", "Email", "LastVisited", "Name", "PhoneNumber", "TotalAMountSpent", "UpdatedOn", "UserId" },
                values: new object[] { 2L, "kfc road 2", new DateTime(2021, 3, 2, 3, 27, 27, 796, DateTimeKind.Local).AddTicks(2356), "mckensie2@gmail.com", new DateTime(2021, 3, 4, 3, 27, 27, 796, DateTimeKind.Local).AddTicks(2427), "William 2", "08109502100", 500000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2L });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedOn", "Email", "LastVisited", "Name", "PhoneNumber", "TotalAMountSpent", "UpdatedOn", "UserId" },
                values: new object[] { 1L, "kfc road 4", new DateTime(2018, 10, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "mckensie4@gmail.com", new DateTime(2021, 3, 4, 3, 27, 27, 792, DateTimeKind.Local).AddTicks(783), "William 4", "08109502104", 500000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4L });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "CreatedOn", "Email", "LastVisited", "Name", "PhoneNumber", "TotalAMountSpent", "UpdatedOn", "UserId" },
                values: new object[] { 3L, "kfc road 5", new DateTime(2020, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "mckensie5@gmail.com", new DateTime(2021, 3, 4, 3, 27, 27, 796, DateTimeKind.Local).AddTicks(2447), "William 5", "08109502105", 500000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5L });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PhoneNumber_Name",
                table: "Customers",
                columns: new[] { "PhoneNumber", "Name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Discounts_Name_DiscountType",
                table: "Discounts",
                columns: new[] { "Name", "DiscountType" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_DiscountId",
                table: "Invoices",
                column: "DiscountId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Item",
                table: "Invoices",
                column: "Item");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PhoneNumber_Name",
                table: "Users",
                columns: new[] { "PhoneNumber", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Discounts");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

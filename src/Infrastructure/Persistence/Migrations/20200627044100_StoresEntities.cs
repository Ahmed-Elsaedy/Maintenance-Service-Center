using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElarabyCA.Infrastructure.Persistence.Migrations
{
    public partial class StoresEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Store",
                columns: table => new
                {
                    StoreId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Administrator = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Store", x => x.StoreId);
                    table.ForeignKey(
                        name: "FK_Store_Employee",
                        column: x => x.Administrator,
                        principalTable: "Employee",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ValueGroup",
                columns: table => new
                {
                    ValueGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(maxLength: 450, nullable: true),
                    Group = table.Column<string>(maxLength: 450, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValueGroup", x => x.ValueGroupId);
                });

            migrationBuilder.CreateTable(
                name: "SparePart",
                columns: table => new
                {
                    SparePartId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Barcode = table.Column<string>(maxLength: 200, nullable: true),
                    Title = table.Column<string>(nullable: true),
                    Category = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SparePart", x => x.SparePartId);
                    table.ForeignKey(
                        name: "FK_SparePart_ValueObject",
                        column: x => x.Category,
                        principalTable: "ValueGroup",
                        principalColumn: "ValueGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreId = table.Column<int>(nullable: false),
                    SparePartId = table.Column<int>(nullable: false),
                    OpeningBalance = table.Column<int>(nullable: false),
                    CurrentBalance = table.Column<int>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryId);
                    table.ForeignKey(
                        name: "FK_Inventory_SparePart",
                        column: x => x.SparePartId,
                        principalTable: "SparePart",
                        principalColumn: "SparePartId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Inventory_Store",
                        column: x => x.StoreId,
                        principalTable: "Store",
                        principalColumn: "StoreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InventoryTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryId = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: true),
                    Amount = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ReferenceType = table.Column<int>(nullable: true),
                    ReferenceId = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_Inventory",
                        column: x => x.InventoryId,
                        principalTable: "Inventory",
                        principalColumn: "InventoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InventoryTransaction_ValueObject",
                        column: x => x.Type,
                        principalTable: "ValueGroup",
                        principalColumn: "ValueGroupId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_SparePartId",
                table: "Inventory",
                column: "SparePartId");

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_StoreId",
                table: "Inventory",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_InventoryId",
                table: "InventoryTransaction",
                column: "InventoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryTransaction_Type",
                table: "InventoryTransaction",
                column: "Type");

            migrationBuilder.CreateIndex(
                name: "IX_SparePart_Category",
                table: "SparePart",
                column: "Category");

            migrationBuilder.CreateIndex(
                name: "IX_Store_Administrator",
                table: "Store",
                column: "Administrator");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InventoryTransaction");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "SparePart");

            migrationBuilder.DropTable(
                name: "Store");

            migrationBuilder.DropTable(
                name: "ValueGroup");
        }
    }
}

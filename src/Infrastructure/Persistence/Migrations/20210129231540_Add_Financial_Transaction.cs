using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElarabyCA.Infrastructure.Persistence.Migrations
{
    public partial class Add_Financial_Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FinancialTransaction",
                columns: table => new
                {
                    TransactionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedBy = table.Column<string>(maxLength: 450, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    LastModifiedBy = table.Column<string>(maxLength: 450, nullable: true),
                    LastModified = table.Column<DateTime>(maxLength: 450, nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Remarks = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialTransaction", x => x.TransactionId);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_Employee",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FinancialTransaction_ValueObject",
                        column: x => x.Type,
                        principalTable: "ValueGroup",
                        principalColumn: "ValueGroupId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_EmployeeId",
                table: "FinancialTransaction",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FinancialTransaction_Type",
                table: "FinancialTransaction",
                column: "Type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FinancialTransaction");
        }
    }
}

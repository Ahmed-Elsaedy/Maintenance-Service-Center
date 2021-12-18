using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElarabyCA.Infrastructure.Persistence.Migrations
{
    public partial class SMSInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SMSMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Text = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMSMessage", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OrderSMSMessage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(nullable: false),
                    SMSMessageId = table.Column<int>(nullable: false),
                    Sent = table.Column<bool>(nullable: false),
                    Report = table.Column<string>(nullable: true),
                    CreatedBy = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: true),
                    LastModifiedBy = table.Column<string>(nullable: true),
                    LastModified = table.Column<DateTime>(nullable: true),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderSMSMessage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderSMSMessage_Order_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Order",
                        principalColumn: "OID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderSMSMessage_SMSMessage_SMSMessageId",
                        column: x => x.SMSMessageId,
                        principalTable: "SMSMessage",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderSMSMessage_OrderId",
                table: "OrderSMSMessage",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderSMSMessage_SMSMessageId",
                table: "OrderSMSMessage",
                column: "SMSMessageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderSMSMessage");

            migrationBuilder.DropTable(
                name: "SMSMessage");
        }
    }
}

using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ElarabyCA.Infrastructure.Persistence.Migrations
{
    public partial class OrderSMS_Sent_Phone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropPrimaryKey(
            //    name: "PK_Employee",
            //    table: "Employee");

            migrationBuilder.DropColumn(
                name: "Sent",
                table: "OrderSMSMessage");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastSent",
                table: "OrderSMSMessage",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Phone",
                table: "OrderSMSMessage",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SendCount",
                table: "OrderSMSMessage",
                nullable: false,
                defaultValue: 0);

            //migrationBuilder.AddPrimaryKey(
            //    name: "OID",
            //    table: "Employee",
            //    column: "OID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "OID",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "LastSent",
                table: "OrderSMSMessage");

            migrationBuilder.DropColumn(
                name: "Phone",
                table: "OrderSMSMessage");

            migrationBuilder.DropColumn(
                name: "SendCount",
                table: "OrderSMSMessage");

            migrationBuilder.AddColumn<bool>(
                name: "Sent",
                table: "OrderSMSMessage",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Employee",
                table: "Employee",
                column: "OID");
        }
    }
}

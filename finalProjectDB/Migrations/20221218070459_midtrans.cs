using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalProjectDB.Migrations
{
    /// <inheritdoc />
    public partial class midtrans : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Bank",
                table: "Bill",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OrderId",
                table: "Bill",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PaymentType",
                table: "Bill",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TransactionStatus",
                table: "Bill",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "TransactionTime",
                table: "Bill",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "VaNumber",
                table: "Bill",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bank",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "PaymentType",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "TransactionStatus",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "TransactionTime",
                table: "Bill");

            migrationBuilder.DropColumn(
                name: "VaNumber",
                table: "Bill");
        }
    }
}

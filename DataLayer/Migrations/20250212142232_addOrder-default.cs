using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addOrderdefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productOrder_Products_ProductId",
                table: "productOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_productOrder_order_OrderId",
                table: "productOrder");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "productOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "productOrder",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "order",
                type: "datetime2",
                nullable: true,
                defaultValue: new DateTime(2025, 2, 12, 16, 22, 31, 865, DateTimeKind.Local).AddTicks(7123),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_productOrder_Products_ProductId",
                table: "productOrder",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_productOrder_order_OrderId",
                table: "productOrder",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_productOrder_Products_ProductId",
                table: "productOrder");

            migrationBuilder.DropForeignKey(
                name: "FK_productOrder_order_OrderId",
                table: "productOrder");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "productOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "productOrder",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "order",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true,
                oldDefaultValue: new DateTime(2025, 2, 12, 16, 22, 31, 865, DateTimeKind.Local).AddTicks(7123));

            migrationBuilder.AddForeignKey(
                name: "FK_productOrder_Products_ProductId",
                table: "productOrder",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_productOrder_order_OrderId",
                table: "productOrder",
                column: "OrderId",
                principalTable: "order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

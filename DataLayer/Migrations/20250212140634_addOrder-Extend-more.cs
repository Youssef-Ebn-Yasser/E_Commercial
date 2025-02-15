using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    /// <inheritdoc />
    public partial class addOrderExtendmore : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_AspNetUsers_UserId1",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_UserId1",
                table: "order");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "productOrder");

            migrationBuilder.DropColumn(
                name: "UserId1",
                table: "order");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "order",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.CreateIndex(
                name: "IX_order_UserId",
                table: "order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_order_AspNetUsers_UserId",
                table: "order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_order_AspNetUsers_UserId",
                table: "order");

            migrationBuilder.DropIndex(
                name: "IX_order_UserId",
                table: "order");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "productOrder",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "order",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "UserId1",
                table: "order",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_UserId1",
                table: "order",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_order_AspNetUsers_UserId1",
                table: "order",
                column: "UserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}

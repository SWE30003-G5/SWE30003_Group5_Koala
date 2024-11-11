using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWE30003_Group5_Koala.Migrations
{
    /// <inheritdoc />
    public partial class RenameOrderItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_MenuItems_MenuItemID",
                table: "OrederItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrederItems_Orders_OrderID",
                table: "OrederItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrederItems",
                table: "OrederItems");

            migrationBuilder.RenameTable(
                name: "OrederItems",
                newName: "OrderItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_OrderID",
                table: "OrderItems",
                newName: "IX_OrderItems_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItems_MenuItemID",
                table: "OrderItems",
                newName: "IX_OrderItems_MenuItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_MenuItems_MenuItemID",
                table: "OrderItems",
                column: "MenuItemID",
                principalTable: "MenuItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItems_Orders_OrderID",
                table: "OrderItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_MenuItems_MenuItemID",
                table: "OrderItems");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItems_Orders_OrderID",
                table: "OrderItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItems",
                table: "OrderItems");

            migrationBuilder.RenameTable(
                name: "OrderItems",
                newName: "OrederItems");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_OrderID",
                table: "OrederItems",
                newName: "IX_OrederItems_OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItems_MenuItemID",
                table: "OrederItems",
                newName: "IX_OrederItems_MenuItemID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrederItems",
                table: "OrederItems",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_MenuItems_MenuItemID",
                table: "OrederItems",
                column: "MenuItemID",
                principalTable: "MenuItems",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItems_Orders_OrderID",
                table: "OrederItems",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWE30003_Group5_Koala.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageLocation",
                table: "MenuItems",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageLocation",
                table: "MenuItems");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SWE30003_Group5_Koala.Migrations
{
    /// <inheritdoc />
    public partial class FixOrderTypo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tyoe",
                table: "Orders",
                newName: "Type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Orders",
                newName: "Tyoe");
        }
    }
}

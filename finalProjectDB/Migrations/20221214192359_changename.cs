using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalProjectDB.Migrations
{
    /// <inheritdoc />
    public partial class changename : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarePrice_id",
                table: "CarePrice",
                newName: "CarePriceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CarePriceId",
                table: "CarePrice",
                newName: "CarePrice_id");
        }
    }
}

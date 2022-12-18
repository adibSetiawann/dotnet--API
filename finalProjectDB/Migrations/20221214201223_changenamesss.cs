using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalProjectDB.Migrations
{
    /// <inheritdoc />
    public partial class changenamesss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CareType_id",
                table: "CareType",
                newName: "CareTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CareTypeId",
                table: "CareType",
                newName: "CareType_id");
        }
    }
}

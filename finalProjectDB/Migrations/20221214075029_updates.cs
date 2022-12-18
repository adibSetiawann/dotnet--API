using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalProjectDB.Migrations
{
    /// <inheritdoc />
    public partial class updates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Description",
                table: "City",
                newName: "Descriptionn");
            migrationBuilder.Sql(
                "CREATE FUNCTION \"update_date_doctor\"() RETURNS TRIGGER LANGUAGE PLPGSQL AS $$ BEGIN NEW.\"LastUpdated\" := now(); RETURN NEW; END; $$; CREATE TRIGGER \"UpdateTimestamp\" BEFORE INSERT OR UPDATE ON \"Doctor\" FOR EACH ROW EXECUTE FUNCTION \"update_date_doctor\"();"
            );
            migrationBuilder.Sql(
                "CREATE FUNCTION \"update_date_Customer\"() RETURNS TRIGGER LANGUAGE PLPGSQL AS $$ BEGIN NEW.\"LastUpdated\" := now(); RETURN NEW; END; $$; CREATE TRIGGER \"UpdateTimestamp\" BEFORE INSERT OR UPDATE ON \"Customer\" FOR EACH ROW EXECUTE FUNCTION \"update_date_Customer\"();"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descriptionn",
                table: "City",
                newName: "Description");
        }
    }
}

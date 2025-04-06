using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlearviewQuotes.Migrations
{
    public partial class FixSpelling : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BuisnessUnitId",
                table: "Accounts",
                newName: "BusinessUnitId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "BusinessUnitId",
                table: "Accounts",
                newName: "BuisnessUnitId");
        }
    }
}

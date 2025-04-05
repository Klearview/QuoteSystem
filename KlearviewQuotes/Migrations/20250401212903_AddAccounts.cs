using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlearviewQuotes.Migrations
{
    public partial class AddAccounts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<string>(type: "TEXT", nullable: false),
                    BuisnessUnitId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<string>(type: "TEXT", nullable: true),
                    TaxExempt = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultServiceLocationId = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultBillingLocationId = table.Column<string>(type: "TEXT", nullable: true),
                    AccountType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlearviewQuotes.Migrations
{
    public partial class AddAgreements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    AgreementId = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceLocationId = table.Column<string>(type: "TEXT", nullable: false),
                    BillingLocationId = table.Column<string>(type: "TEXT", nullable: true),
                    BusinessUnitId = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultServiceContactId = table.Column<string>(type: "TEXT", nullable: true),
                    DefaultBillingContactId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Issue = table.Column<string>(type: "TEXT", nullable: true),
                    CancelDate = table.Column<string>(type: "TEXT", nullable: true),
                    CancelReason = table.Column<string>(type: "TEXT", nullable: true),
                    CompletedOn = table.Column<string>(type: "TEXT", nullable: true),
                    StartDate = table.Column<string>(type: "TEXT", nullable: true),
                    EndDate = table.Column<string>(type: "TEXT", nullable: true),
                    ContractName = table.Column<string>(type: "TEXT", nullable: true),
                    ContractPeriodAmount = table.Column<string>(type: "TEXT", nullable: true),
                    ContractChargeForProducts = table.Column<string>(type: "TEXT", nullable: true),
                    ContractChargeForServices = table.Column<string>(type: "TEXT", nullable: true),
                    ContractProrated = table.Column<string>(type: "TEXT", nullable: true),
                    ContractAmountTaxable = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceInitialStartDate = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceInitialEndDate = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceSchedulePeriod = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceScheduleDelay = table.Column<string>(type: "TEXT", nullable: true),
                    UpfrontInvoicingDaysAhead = table.Column<string>(type: "TEXT", nullable: true),
                    UpfrontInvoicingTimeToCreateInvoice = table.Column<string>(type: "TEXT", nullable: true),
                    CampaignId = table.Column<string>(type: "TEXT", nullable: true),
                    CancelReasonOther = table.Column<string>(type: "TEXT", nullable: true),
                    IsRenewal = table.Column<string>(type: "TEXT", nullable: true),
                    RenewalDate = table.Column<string>(type: "TEXT", nullable: true),
                    RenewalAgreementId = table.Column<string>(type: "TEXT", nullable: true),
                    RenewalStatus = table.Column<string>(type: "TEXT", nullable: true),
                    InvoiceMode = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.AgreementId);
                    table.ForeignKey(
                        name: "FK_Agreements_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Agreements_ServiceLocations_ServiceLocationId",
                        column: x => x.ServiceLocationId,
                        principalTable: "ServiceLocations",
                        principalColumn: "ServiceLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zones",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    BusinessUnitId = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Color = table.Column<string>(type: "TEXT", nullable: true),
                    Active = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zones", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceLocations_ZoneId",
                table: "ServiceLocations",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_AccountId",
                table: "Agreements",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Agreements_ServiceLocationId",
                table: "Agreements",
                column: "ServiceLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLocations_Zones_ZoneId",
                table: "ServiceLocations",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLocations_Zones_ZoneId",
                table: "ServiceLocations");

            migrationBuilder.DropTable(
                name: "Agreements");

            migrationBuilder.DropTable(
                name: "Zones");

            migrationBuilder.DropIndex(
                name: "IX_ServiceLocations_ZoneId",
                table: "ServiceLocations");
        }
    }
}

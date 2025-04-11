using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlearviewQuotes.Migrations
{
    public partial class ModifyWorkOrderTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLocations_Accounts_AccountId",
                table: "ServiceLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLocations_Zones_ZoneId",
                table: "ServiceLocations");

            migrationBuilder.AlterColumn<string>(
                name: "ZoneId",
                table: "ServiceLocations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "ServiceLocations",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "WorkOrders",
                columns: table => new
                {
                    WorkOrderId = table.Column<string>(type: "TEXT", nullable: false),
                    AccountId = table.Column<string>(type: "TEXT", nullable: false),
                    ServiceLocationId = table.Column<string>(type: "TEXT", nullable: false),
                    BillingLocationId = table.Column<string>(type: "TEXT", nullable: true),
                    AgreementId = table.Column<string>(type: "TEXT", nullable: true),
                    BusinessUnitId = table.Column<string>(type: "TEXT", nullable: true),
                    Number = table.Column<int>(type: "INTEGER", nullable: true),
                    Issue = table.Column<string>(type: "TEXT", nullable: true),
                    DurationAdjustment = table.Column<string>(type: "TEXT", nullable: true),
                    ExpectedDuration = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceSubTotal = table.Column<string>(type: "TEXT", nullable: true),
                    ProductSubTotal = table.Column<string>(type: "TEXT", nullable: true),
                    SubTotal = table.Column<int>(type: "INTEGER", nullable: true),
                    ResourceId = table.Column<string>(type: "TEXT", nullable: true),
                    CommittedResourceId = table.Column<string>(type: "TEXT", nullable: true),
                    EstimatedStartTime = table.Column<string>(type: "TEXT", nullable: true),
                    EstimatedTimeOfArrival = table.Column<string>(type: "TEXT", nullable: true),
                    EstimatedCompletedTime = table.Column<string>(type: "TEXT", nullable: true),
                    DepartureTime = table.Column<string>(type: "TEXT", nullable: true),
                    ActualStartTime = table.Column<string>(type: "TEXT", nullable: true),
                    ActualCompletedTime = table.Column<string>(type: "TEXT", nullable: true),
                    StartOfCommitmentWindow = table.Column<string>(type: "TEXT", nullable: true),
                    EndOfCommitmentWindow = table.Column<string>(type: "TEXT", nullable: true),
                    StartingEligibleDate = table.Column<string>(type: "TEXT", nullable: true),
                    EndingEligibleDate = table.Column<string>(type: "TEXT", nullable: true),
                    StartTimePreference = table.Column<string>(type: "TEXT", nullable: true),
                    EndTimePreference = table.Column<string>(type: "TEXT", nullable: true),
                    PreferredResourceId = table.Column<string>(type: "TEXT", nullable: true),
                    DayOfWeekPreference = table.Column<string>(type: "TEXT", nullable: true),
                    StartedBy = table.Column<string>(type: "TEXT", nullable: true),
                    CompletedBy = table.Column<string>(type: "TEXT", nullable: true),
                    UnassignedResourceId = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    Closed = table.Column<string>(type: "TEXT", nullable: true),
                    ScheduledDate = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceComment = table.Column<string>(type: "TEXT", nullable: true),
                    TechComment = table.Column<string>(type: "TEXT", nullable: true),
                    CancelledDate = table.Column<string>(type: "TEXT", nullable: true),
                    CancelledTime = table.Column<string>(type: "TEXT", nullable: true),
                    IssueNote = table.Column<string>(type: "TEXT", nullable: true),
                    PerformedByResourceId = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceLocationServiceMemo = table.Column<string>(type: "TEXT", nullable: true),
                    BillingLocationBillingMemo = table.Column<string>(type: "TEXT", nullable: true),
                    CampaignId = table.Column<string>(type: "TEXT", nullable: true),
                    CancelReason = table.Column<string>(type: "TEXT", nullable: true),
                    CancelReasonOther = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkOrders", x => x.WorkOrderId);
                    table.ForeignKey(
                        name: "FK_WorkOrders_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkOrders_ServiceLocations_ServiceLocationId",
                        column: x => x.ServiceLocationId,
                        principalTable: "ServiceLocations",
                        principalColumn: "ServiceLocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_AccountId",
                table: "WorkOrders",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkOrders_ServiceLocationId",
                table: "WorkOrders",
                column: "ServiceLocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLocations_Accounts_AccountId",
                table: "ServiceLocations",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLocations_Zones_ZoneId",
                table: "ServiceLocations",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLocations_Accounts_AccountId",
                table: "ServiceLocations");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceLocations_Zones_ZoneId",
                table: "ServiceLocations");

            migrationBuilder.DropTable(
                name: "WorkOrders");

            migrationBuilder.AlterColumn<string>(
                name: "ZoneId",
                table: "ServiceLocations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "AccountId",
                table: "ServiceLocations",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLocations_Accounts_AccountId",
                table: "ServiceLocations",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceLocations_Zones_ZoneId",
                table: "ServiceLocations",
                column: "ZoneId",
                principalTable: "Zones",
                principalColumn: "Id");
        }
    }
}

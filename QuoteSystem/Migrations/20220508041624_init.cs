using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuoteSystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Quotes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SentAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SentBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EstimateXML = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    CustomerInfoXML = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    QuoteInfoXML = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quotes", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Quotes");
        }
    }
}

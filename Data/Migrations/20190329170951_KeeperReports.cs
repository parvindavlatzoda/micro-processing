using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MP.Data.Migrations
{
    public partial class KeeperReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RubReports",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    QpayTransactionId = table.Column<string>(nullable: true),
                    GatewayTransactionId = table.Column<string>(nullable: true),
                    ServiceProviderTransactionId = table.Column<string>(nullable: true),
                    AmountInTjs = table.Column<decimal>(nullable: false),
                    AmountInRub = table.Column<decimal>(nullable: false),
                    RubRate = table.Column<decimal>(nullable: false),
                    Account = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    QpayCreatedAt = table.Column<DateTime>(nullable: false),
                    QpayPayedAt = table.Column<DateTime>(nullable: false),
                    TerminalNumber = table.Column<string>(nullable: true),
                    AgentNumber = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RubReports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RubReports");
        }
    }
}

using Microsoft.EntityFrameworkCore.Migrations;

namespace MP.Data.Migrations
{
    public partial class AddedServiceTitle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ServiceTitle",
                table: "RubReports",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceUpgId",
                table: "RubReports",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ServiceTitle",
                table: "RubReports");

            migrationBuilder.DropColumn(
                name: "ServiceUpgId",
                table: "RubReports");
        }
    }
}

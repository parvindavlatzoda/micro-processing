using Microsoft.EntityFrameworkCore.Migrations;

namespace MP.Data.Migrations
{
    public partial class AddedStatusToRubReports : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RubReports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "RubReports");
        }
    }
}

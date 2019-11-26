using Microsoft.EntityFrameworkCore.Migrations;

namespace CrystalProcess.Data.Migrations
{
    public partial class addingTitleOrderToStage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Stages",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Stages",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "Stages");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Stages");
        }
    }
}

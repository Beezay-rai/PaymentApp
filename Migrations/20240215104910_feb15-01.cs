using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentApp.Migrations
{
    public partial class feb1501 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Transaction");
        }
    }
}

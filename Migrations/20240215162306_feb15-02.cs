using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PaymentApp.Migrations
{
    public partial class feb1502 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "TrackingId",
                table: "Transaction",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrackingId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

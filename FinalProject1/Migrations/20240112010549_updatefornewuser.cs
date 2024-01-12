using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject1.Migrations
{
    public partial class updatefornewuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userId",
                table: "Songs");

            migrationBuilder.AddColumn<string>(
                name: "authId",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "authId",
                table: "Songs");

            migrationBuilder.AddColumn<int>(
                name: "userId",
                table: "Songs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

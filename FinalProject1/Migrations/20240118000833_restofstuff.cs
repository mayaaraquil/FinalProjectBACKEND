using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalProject1.Migrations
{
    public partial class restofstuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "BlogPost",
                newName: "BlogTitle");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "BlogPost",
                newName: "BlogContent");

            migrationBuilder.AddColumn<int>(
                name: "AuthId",
                table: "Videos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "AuthorId",
                table: "Replies",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "AuthId",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "BlogPost",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Videos");

            migrationBuilder.DropColumn(
                name: "AuthId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "BlogTitle",
                table: "BlogPost",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "BlogContent",
                table: "BlogPost",
                newName: "Content");

            migrationBuilder.AlterColumn<int>(
                name: "AuthorId",
                table: "Replies",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "BlogPost",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}

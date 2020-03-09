using Microsoft.EntityFrameworkCore.Migrations;

namespace Kaysho.NET.API.Migrations
{
    public partial class AddedUserToBlog : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "User",
                table: "Blogs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "User",
                table: "Blogs");
        }
    }
}

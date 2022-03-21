using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class V4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "WorksIn",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "WorksIn");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "User",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

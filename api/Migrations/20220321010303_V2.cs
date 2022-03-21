using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class V2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Store_StoreID",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_StoreID",
                table: "User");

            migrationBuilder.DropColumn(
                name: "StoreID",
                table: "User");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "StoreUser",
                columns: table => new
                {
                    StaffID = table.Column<int>(type: "int", nullable: false),
                    WorkplacesID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StoreUser", x => new { x.StaffID, x.WorkplacesID });
                    table.ForeignKey(
                        name: "FK_StoreUser_Store_WorkplacesID",
                        column: x => x.WorkplacesID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StoreUser_User_StaffID",
                        column: x => x.StaffID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StoreUser_WorkplacesID",
                table: "StoreUser",
                column: "WorkplacesID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreUser");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "User");

            migrationBuilder.AddColumn<int>(
                name: "StoreID",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_User_StoreID",
                table: "User",
                column: "StoreID");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Store_StoreID",
                table: "User",
                column: "StoreID",
                principalTable: "Store",
                principalColumn: "ID");
        }
    }
}

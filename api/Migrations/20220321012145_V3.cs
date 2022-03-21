using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    public partial class V3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StoreUser");

            migrationBuilder.CreateTable(
                name: "WorksIn",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    StoreID = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorksIn", x => x.ID);
                    table.ForeignKey(
                        name: "FK_WorksIn_Store_StoreID",
                        column: x => x.StoreID,
                        principalTable: "Store",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorksIn_User_UserID",
                        column: x => x.UserID,
                        principalTable: "User",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorksIn_StoreID",
                table: "WorksIn",
                column: "StoreID");

            migrationBuilder.CreateIndex(
                name: "IX_WorksIn_UserID",
                table: "WorksIn",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorksIn");

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
    }
}

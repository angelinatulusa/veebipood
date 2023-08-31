using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace veebipood.Migrations
{
    /// <inheritdoc />
    public partial class updateOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "CartProducts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TotalSum = table.Column<double>(type: "float", nullable: false),
                    Paid = table.Column<bool>(type: "bit", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_PersonId",
                table: "Orders",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartProducts_Orders_OrderId",
                table: "CartProducts");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_CartProducts_OrderId",
                table: "CartProducts");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "CartProducts");
        }
    }
}

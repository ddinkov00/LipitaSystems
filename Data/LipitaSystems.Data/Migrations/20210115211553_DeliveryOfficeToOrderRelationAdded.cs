using Microsoft.EntityFrameworkCore.Migrations;

namespace LipitaSystems.Data.Migrations
{
    public partial class DeliveryOfficeToOrderRelationAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DeliveryOfficeId",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_DeliveryOfficeId",
                table: "Orders",
                column: "DeliveryOfficeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_DeliveryOffices_DeliveryOfficeId",
                table: "Orders",
                column: "DeliveryOfficeId",
                principalTable: "DeliveryOffices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_DeliveryOffices_DeliveryOfficeId",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_DeliveryOfficeId",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "DeliveryOfficeId",
                table: "Orders");
        }
    }
}

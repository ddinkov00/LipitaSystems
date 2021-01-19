using Microsoft.EntityFrameworkCore.Migrations;

namespace LipitaSystems.Data.Migrations
{
    public partial class DiscountCodeRelationsChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCodeSecondaryCategory");

            migrationBuilder.AddColumn<string>(
                name: "DiscountCodeName",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MainCategoryId",
                table: "DiscountCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_MainCategoryId",
                table: "DiscountCodes",
                column: "MainCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_DiscountCodes_MainCategories_MainCategoryId",
                table: "DiscountCodes",
                column: "MainCategoryId",
                principalTable: "MainCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DiscountCodes_MainCategories_MainCategoryId",
                table: "DiscountCodes");

            migrationBuilder.DropIndex(
                name: "IX_DiscountCodes_MainCategoryId",
                table: "DiscountCodes");

            migrationBuilder.DropColumn(
                name: "DiscountCodeName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "MainCategoryId",
                table: "DiscountCodes");

            migrationBuilder.CreateTable(
                name: "DiscountCodeSecondaryCategory",
                columns: table => new
                {
                    DiscountCodesId = table.Column<int>(type: "int", nullable: false),
                    SecondaryCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodeSecondaryCategory", x => new { x.DiscountCodesId, x.SecondaryCategoriesId });
                    table.ForeignKey(
                        name: "FK_DiscountCodeSecondaryCategory_DiscountCodes_DiscountCodesId",
                        column: x => x.DiscountCodesId,
                        principalTable: "DiscountCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountCodeSecondaryCategory_SecondaryCategories_SecondaryCategoriesId",
                        column: x => x.SecondaryCategoriesId,
                        principalTable: "SecondaryCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeSecondaryCategory_SecondaryCategoriesId",
                table: "DiscountCodeSecondaryCategory",
                column: "SecondaryCategoriesId");
        }
    }
}

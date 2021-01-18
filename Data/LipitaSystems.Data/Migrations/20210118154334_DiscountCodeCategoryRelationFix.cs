using Microsoft.EntityFrameworkCore.Migrations;

namespace LipitaSystems.Data.Migrations
{
    public partial class DiscountCodeCategoryRelationFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SecondaryCategories_DiscountCodes_DiscountCodeId",
                table: "SecondaryCategories");

            migrationBuilder.DropIndex(
                name: "IX_SecondaryCategories_DiscountCodeId",
                table: "SecondaryCategories");

            migrationBuilder.DropColumn(
                name: "DiscountCodeId",
                table: "SecondaryCategories");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCodeSecondaryCategory");

            migrationBuilder.AddColumn<int>(
                name: "DiscountCodeId",
                table: "SecondaryCategories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SecondaryCategories_DiscountCodeId",
                table: "SecondaryCategories",
                column: "DiscountCodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SecondaryCategories_DiscountCodes_DiscountCodeId",
                table: "SecondaryCategories",
                column: "DiscountCodeId",
                principalTable: "DiscountCodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

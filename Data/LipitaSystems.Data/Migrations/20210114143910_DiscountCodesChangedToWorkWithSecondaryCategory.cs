using Microsoft.EntityFrameworkCore.Migrations;

namespace LipitaSystems.Data.Migrations
{
    public partial class DiscountCodesChangedToWorkWithSecondaryCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCodeMainCategory");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "DiscountCodeMainCategory",
                columns: table => new
                {
                    DiscountCodesId = table.Column<int>(type: "int", nullable: false),
                    MainCategoriesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodeMainCategory", x => new { x.DiscountCodesId, x.MainCategoriesId });
                    table.ForeignKey(
                        name: "FK_DiscountCodeMainCategory_DiscountCodes_DiscountCodesId",
                        column: x => x.DiscountCodesId,
                        principalTable: "DiscountCodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DiscountCodeMainCategory_MainCategories_MainCategoriesId",
                        column: x => x.MainCategoriesId,
                        principalTable: "MainCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodeMainCategory_MainCategoriesId",
                table: "DiscountCodeMainCategory",
                column: "MainCategoriesId");
        }
    }
}

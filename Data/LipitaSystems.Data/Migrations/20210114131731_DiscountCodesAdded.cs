using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LipitaSystems.Data.Migrations
{
    public partial class DiscountCodesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DiscountCodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DiscountPercentage = table.Column<int>(type: "int", nullable: false),
                    DoesWorkOnDiscountedProducts = table.Column<bool>(type: "bit", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiscountCodes", x => x.Id);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_DiscountCodes_IsDeleted",
                table: "DiscountCodes",
                column: "IsDeleted");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DiscountCodeMainCategory");

            migrationBuilder.DropTable(
                name: "DiscountCodes");
        }
    }
}

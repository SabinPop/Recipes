using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.API.Migrations
{
    public partial class _4thMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientWithQuantityEntity_IngredientEntities_IngredientId",
                table: "IngredientWithQuantityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalValues_IngredientEntities_IngredientId",
                table: "NutritionalValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientEntities",
                table: "IngredientEntities");

            migrationBuilder.RenameTable(
                name: "IngredientEntities",
                newName: "Ingredients");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientWithQuantityEntity_Ingredients_IngredientId",
                table: "IngredientWithQuantityEntity",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalValues_Ingredients_IngredientId",
                table: "NutritionalValues",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IngredientWithQuantityEntity_Ingredients_IngredientId",
                table: "IngredientWithQuantityEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalValues_Ingredients_IngredientId",
                table: "NutritionalValues");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "IngredientEntities");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientEntities",
                table: "IngredientEntities",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_IngredientWithQuantityEntity_IngredientEntities_IngredientId",
                table: "IngredientWithQuantityEntity",
                column: "IngredientId",
                principalTable: "IngredientEntities",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalValues_IngredientEntities_IngredientId",
                table: "NutritionalValues",
                column: "IngredientId",
                principalTable: "IngredientEntities",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

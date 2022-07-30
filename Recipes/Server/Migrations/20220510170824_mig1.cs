using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Server.Migrations
{
    public partial class mig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalValues_Ingredients_IngredientId",
                table: "NutritionalValues");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalValuesRecipeEntity_Recipes_RecipeId",
                table: "NutritionalValuesRecipeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionalValuesRecipeEntity",
                table: "NutritionalValuesRecipeEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionalValues",
                table: "NutritionalValues");

            migrationBuilder.RenameTable(
                name: "NutritionalValuesRecipeEntity",
                newName: "NutritionalValuesRecipe");

            migrationBuilder.RenameTable(
                name: "NutritionalValues",
                newName: "NutritionalValuesIngredient");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionalValuesRecipeEntity_RecipeId",
                table: "NutritionalValuesRecipe",
                newName: "IX_NutritionalValuesRecipe_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionalValues_IngredientId",
                table: "NutritionalValuesIngredient",
                newName: "IX_NutritionalValuesIngredient_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionalValuesRecipe",
                table: "NutritionalValuesRecipe",
                column: "NutritionalValuesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionalValuesIngredient",
                table: "NutritionalValuesIngredient",
                column: "NutritionalValuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalValuesIngredient_Ingredients_IngredientId",
                table: "NutritionalValuesIngredient",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalValuesRecipe_Recipes_RecipeId",
                table: "NutritionalValuesRecipe",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalValuesIngredient_Ingredients_IngredientId",
                table: "NutritionalValuesIngredient");

            migrationBuilder.DropForeignKey(
                name: "FK_NutritionalValuesRecipe_Recipes_RecipeId",
                table: "NutritionalValuesRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionalValuesRecipe",
                table: "NutritionalValuesRecipe");

            migrationBuilder.DropPrimaryKey(
                name: "PK_NutritionalValuesIngredient",
                table: "NutritionalValuesIngredient");

            migrationBuilder.RenameTable(
                name: "NutritionalValuesRecipe",
                newName: "NutritionalValuesRecipeEntity");

            migrationBuilder.RenameTable(
                name: "NutritionalValuesIngredient",
                newName: "NutritionalValues");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionalValuesRecipe_RecipeId",
                table: "NutritionalValuesRecipeEntity",
                newName: "IX_NutritionalValuesRecipeEntity_RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_NutritionalValuesIngredient_IngredientId",
                table: "NutritionalValues",
                newName: "IX_NutritionalValues_IngredientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionalValuesRecipeEntity",
                table: "NutritionalValuesRecipeEntity",
                column: "NutritionalValuesId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_NutritionalValues",
                table: "NutritionalValues",
                column: "NutritionalValuesId");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalValues_Ingredients_IngredientId",
                table: "NutritionalValues",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionalValuesRecipeEntity_Recipes_RecipeId",
                table: "NutritionalValuesRecipeEntity",
                column: "RecipeId",
                principalTable: "Recipes",
                principalColumn: "RecipeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}

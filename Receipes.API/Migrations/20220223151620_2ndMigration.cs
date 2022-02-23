using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.API.Migrations
{
    public partial class _2ndMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_IngredientEntities_IngredientEntityIngredientId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_IngredientEntityIngredientId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "IngredientEntityIngredientId",
                table: "Recipes");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IngredientEntityIngredientId",
                table: "Recipes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IngredientEntityIngredientId",
                table: "Recipes",
                column: "IngredientEntityIngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_IngredientEntities_IngredientEntityIngredientId",
                table: "Recipes",
                column: "IngredientEntityIngredientId",
                principalTable: "IngredientEntities",
                principalColumn: "IngredientId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

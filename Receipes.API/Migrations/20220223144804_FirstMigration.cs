using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.API.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IngredientEntities",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UseWeight = table.Column<bool>(type: "bit", nullable: false),
                    WeightOfSinglePiece = table.Column<decimal>(type: "decimal(6,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientEntities", x => x.IngredientId);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    TagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.TagId);
                });

            migrationBuilder.CreateTable(
                name: "NutritionalValues",
                columns: table => new
                {
                    NutritionalValuesId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Kilocalories = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    Fat = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    Protein = table.Column<decimal>(type: "decimal(5,1)", nullable: false),
                    Carbs = table.Column<decimal>(type: "decimal(5,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionalValues", x => x.NutritionalValuesId);
                    table.ForeignKey(
                        name: "FK_NutritionalValues_IngredientEntities_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "IngredientEntities",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    NumberOfServings = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IngredientEntityIngredientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.ForeignKey(
                        name: "FK_Recipes_IngredientEntities_IngredientEntityIngredientId",
                        column: x => x.IngredientEntityIngredientId,
                        principalTable: "IngredientEntities",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "IngredientWithQuantityEntity",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<decimal>(type: "decimal(5,1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientWithQuantityEntity", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_IngredientWithQuantityEntity_IngredientEntities_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "IngredientEntities",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IngredientWithQuantityEntity_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeEntityTagEntity",
                columns: table => new
                {
                    RecipesRecipeId = table.Column<int>(type: "int", nullable: false),
                    TagsTagId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeEntityTagEntity", x => new { x.RecipesRecipeId, x.TagsTagId });
                    table.ForeignKey(
                        name: "FK_RecipeEntityTagEntity_Recipes_RecipesRecipeId",
                        column: x => x.RecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeEntityTagEntity_Tags_TagsTagId",
                        column: x => x.TagsTagId,
                        principalTable: "Tags",
                        principalColumn: "TagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeSteps",
                columns: table => new
                {
                    StepId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    StepTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    StepDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeSteps", x => x.StepId);
                    table.ForeignKey(
                        name: "FK_RecipeSteps_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IngredientWithQuantityEntity_IngredientId",
                table: "IngredientWithQuantityEntity",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionalValues_IngredientId",
                table: "NutritionalValues",
                column: "IngredientId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RecipeEntityTagEntity_TagsTagId",
                table: "RecipeEntityTagEntity",
                column: "TagsTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_IngredientEntityIngredientId",
                table: "Recipes",
                column: "IngredientEntityIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeSteps_RecipeId",
                table: "RecipeSteps",
                column: "RecipeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IngredientWithQuantityEntity");

            migrationBuilder.DropTable(
                name: "NutritionalValues");

            migrationBuilder.DropTable(
                name: "RecipeEntityTagEntity");

            migrationBuilder.DropTable(
                name: "RecipeSteps");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "IngredientEntities");
        }
    }
}

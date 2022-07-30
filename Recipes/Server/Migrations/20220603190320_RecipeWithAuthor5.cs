using Microsoft.EntityFrameworkCore.Migrations;

namespace Recipes.Server.Migrations
{
    public partial class RecipeWithAuthor5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                table: "Recipes");

            migrationBuilder.DropIndex(
                name: "IX_Recipes_ApplicationUserId",
                table: "Recipes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Recipes");

            migrationBuilder.CreateTable(
                name: "ApplicationUserRecipeEntity",
                columns: table => new
                {
                    FavoriteRecipesRecipeId = table.Column<int>(type: "int", nullable: false),
                    UsersWhoLikedThisId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUserRecipeEntity", x => new { x.FavoriteRecipesRecipeId, x.UsersWhoLikedThisId });
                    table.ForeignKey(
                        name: "FK_ApplicationUserRecipeEntity_AspNetUsers_UsersWhoLikedThisId",
                        column: x => x.UsersWhoLikedThisId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ApplicationUserRecipeEntity_Recipes_FavoriteRecipesRecipeId",
                        column: x => x.FavoriteRecipesRecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUserRecipeEntity_UsersWhoLikedThisId",
                table: "ApplicationUserRecipeEntity",
                column: "UsersWhoLikedThisId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApplicationUserRecipeEntity");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Recipes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_ApplicationUserId",
                table: "Recipes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Recipes_AspNetUsers_ApplicationUserId",
                table: "Recipes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Recipes.API.Models.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Recipes.API.Data
{
    public class RecipesDbContext : DbContext
    {
        public virtual DbSet<RecipeEntity> Recipes { get; set; }
        public virtual DbSet<IngredientEntity> Ingredients { get; set; }
        public virtual DbSet<NutritionalValuesEntity> NutritionalValues { get; set; }
        public virtual DbSet<RecipeStepEntity> RecipeSteps { get; set; }
        public virtual DbSet<TagEntity> Tags { get; set; }

        public RecipesDbContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        protected RecipesDbContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredientWithQuantityEntity>()
                .HasKey(iq => new { iq.RecipeId, iq.IngredientId });

            modelBuilder.Entity<IngredientWithQuantityEntity>()
                .HasOne(iq => iq.Ingredient)
                .WithMany(i => i.IngredientsWithQuantities);

            modelBuilder.Entity<IngredientEntity>()
                .HasMany(i => i.IngredientsWithQuantities)
                .WithOne(iq => iq.Ingredient);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.IngredientsWithQuantities)
                .WithOne(iq => iq.Recipe);

            modelBuilder.Entity<IngredientWithQuantityEntity>()
                .HasOne(iq => iq.Recipe)
                .WithMany(r => r.IngredientsWithQuantities);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.RecipeSteps)
                .WithOne(rs => rs.Recipe);

            modelBuilder.Entity<RecipeStepEntity>()
                .HasOne(rs => rs.Recipe)
                .WithMany(r => r.RecipeSteps)
                .HasForeignKey(rs => rs.RecipeId);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.Tags)
                .WithMany(t => t.Recipes);

            modelBuilder.Entity<TagEntity>()
                .HasMany(t => t.Recipes)
                .WithMany(r => r.Tags);

            modelBuilder.Entity<IngredientEntity>()
                .HasOne(i => i.NutritionalValues)
                .WithOne(nv => nv.Ingredient);

            modelBuilder.Entity<NutritionalValuesEntity>()
                .HasOne(nv => nv.Ingredient)
                .WithOne(i => i.NutritionalValues);
        }
    }
}

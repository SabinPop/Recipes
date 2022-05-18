using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Recipes.Server.Models;
using Recipes.Server.Models.Entities;

namespace Recipes.Server.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public virtual DbSet<RecipeEntity> Recipes { get; set; }
        public virtual DbSet<IngredientEntity> Ingredients { get; set; }
        public virtual DbSet<NutritionalValuesIngredientEntity> NutritionalValuesIngredient { get; set; }
        public virtual DbSet<NutritionalValuesRecipeEntity> NutritionalValuesRecipe { get; set; }
        public virtual DbSet<RecipeStepEntity> RecipeSteps { get; set; }
        public virtual DbSet<TagEntity> Tags { get; set; }

        // users

        // public virtual DbSet<ApplicationUser> Users { get; set; }


        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*
            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.FavoriteRecipes)
                .WithMany(fr => fr.UsersWhoLikedThis);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.UsersWhoLikedThis)
                .WithMany(a => a.FavoriteRecipes);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(u => u.UserRecipes)
                .WithOne(ur => ur.Author);

            modelBuilder.Entity<RecipeEntity>()
                .HasOne(r => r.Author)
                .WithMany(a => a.UserRecipes)
                .HasForeignKey(r => r.AuthorId);

            */

            modelBuilder.Entity<IngredientWithQuantityEntity>()
                .HasKey(iq => new { iq.RecipeId, iq.IngredientId });


            modelBuilder.Entity<IngredientWithQuantityEntity>()
                .HasOne(iq => iq.Ingredient)
                .WithMany(i => i.IngredientsWithQuantities)
                .HasForeignKey(iq => iq.IngredientId);

            modelBuilder.Entity<IngredientWithQuantityEntity>()
                .HasOne(iq => iq.Recipe)
                .WithMany(r => r.IngredientsWithQuantities)
                .HasForeignKey(iq => iq.RecipeId);


            modelBuilder.Entity<IngredientEntity>()
                .HasMany(i => i.IngredientsWithQuantities)
                .WithOne(iq => iq.Ingredient);

            modelBuilder.Entity<RecipeEntity>()
                .HasMany(r => r.IngredientsWithQuantities)
                .WithOne(iq => iq.Recipe);



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


            // converts Tag name to a lower string to save intro the db
            modelBuilder.Entity<TagEntity>()
                .Property(x => x.Name)
                .HasConversion(n =>
                    n == null ? null : n.ToLower(),
                    n => n);

            modelBuilder.Entity<IngredientEntity>()
                .HasOne(i => i.NutritionalValues)
                .WithOne(nv => nv.Ingredient);

            modelBuilder.Entity<NutritionalValuesIngredientEntity>()
                .HasOne(nv => nv.Ingredient)
                .WithOne(i => i.NutritionalValues)
                .HasForeignKey<NutritionalValuesIngredientEntity>(nv => nv.IngredientId);

            /////

            modelBuilder.Entity<RecipeEntity>()
                .HasOne(r => r.NutritionalValues)
                .WithOne(nv => nv.Recipe);

            modelBuilder.Entity<NutritionalValuesRecipeEntity>()
                .HasOne(nv => nv.Recipe)
                .WithOne(r => r.NutritionalValues)
                .HasForeignKey<NutritionalValuesRecipeEntity>(nv => nv.RecipeId);

            /////
            ///
            modelBuilder.Entity<IngredientEntity>()
                .HasIndex(i => i.Name)
                .IsUnique();

        }
    }
}

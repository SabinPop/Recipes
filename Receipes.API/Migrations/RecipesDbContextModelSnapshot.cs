﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Recipes.API.Data;

namespace Recipes.API.Migrations
{
    [DbContext(typeof(RecipesDbContext))]
    partial class RecipesDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.14")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RecipeEntityTagEntity", b =>
                {
                    b.Property<int>("RecipesRecipeId")
                        .HasColumnType("int");

                    b.Property<int>("TagsTagId")
                        .HasColumnType("int");

                    b.HasKey("RecipesRecipeId", "TagsTagId");

                    b.HasIndex("TagsTagId");

                    b.ToTable("RecipeEntityTagEntity");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.IngredientEntity", b =>
                {
                    b.Property<int>("IngredientId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("UseWeight")
                        .HasColumnType("bit");

                    b.Property<decimal>("WeightOfSinglePiece")
                        .HasColumnType("decimal(6,1)");

                    b.HasKey("IngredientId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.IngredientWithQuantityEntity", b =>
                {
                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Quantity")
                        .HasColumnType("decimal(5,1)");

                    b.HasKey("RecipeId", "IngredientId");

                    b.HasIndex("IngredientId");

                    b.ToTable("IngredientWithQuantityEntity");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.NutritionalValuesEntity", b =>
                {
                    b.Property<int>("NutritionalValuesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<decimal>("Carbs")
                        .HasColumnType("decimal(5,1)");

                    b.Property<decimal>("Fat")
                        .HasColumnType("decimal(5,1)");

                    b.Property<int>("IngredientId")
                        .HasColumnType("int");

                    b.Property<decimal>("Kilocalories")
                        .HasColumnType("decimal(5,1)");

                    b.Property<decimal>("Protein")
                        .HasColumnType("decimal(5,1)");

                    b.HasKey("NutritionalValuesId");

                    b.HasIndex("IngredientId")
                        .IsUnique();

                    b.ToTable("NutritionalValues");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.RecipeEntity", b =>
                {
                    b.Property<int>("RecipeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("DurationInMinutes")
                        .HasColumnType("int");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("NumberOfServings")
                        .HasColumnType("int");

                    b.HasKey("RecipeId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.RecipeStepEntity", b =>
                {
                    b.Property<int>("StepId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("RecipeId")
                        .HasColumnType("int");

                    b.Property<string>("StepDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StepTitle")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("StepId");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeSteps");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.TagEntity", b =>
                {
                    b.Property<int>("TagId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("TagId");

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("RecipeEntityTagEntity", b =>
                {
                    b.HasOne("Recipes.API.Models.Entities.RecipeEntity", null)
                        .WithMany()
                        .HasForeignKey("RecipesRecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipes.API.Models.Entities.TagEntity", null)
                        .WithMany()
                        .HasForeignKey("TagsTagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.IngredientWithQuantityEntity", b =>
                {
                    b.HasOne("Recipes.API.Models.Entities.IngredientEntity", "Ingredient")
                        .WithMany("IngredientsWithQuantities")
                        .HasForeignKey("IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Recipes.API.Models.Entities.RecipeEntity", "Recipe")
                        .WithMany("IngredientsWithQuantities")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.NutritionalValuesEntity", b =>
                {
                    b.HasOne("Recipes.API.Models.Entities.IngredientEntity", "Ingredient")
                        .WithOne("NutritionalValues")
                        .HasForeignKey("Recipes.API.Models.Entities.NutritionalValuesEntity", "IngredientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ingredient");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.RecipeStepEntity", b =>
                {
                    b.HasOne("Recipes.API.Models.Entities.RecipeEntity", "Recipe")
                        .WithMany("RecipeSteps")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.IngredientEntity", b =>
                {
                    b.Navigation("IngredientsWithQuantities");

                    b.Navigation("NutritionalValues");
                });

            modelBuilder.Entity("Recipes.API.Models.Entities.RecipeEntity", b =>
                {
                    b.Navigation("IngredientsWithQuantities");

                    b.Navigation("RecipeSteps");
                });
#pragma warning restore 612, 618
        }
    }
}

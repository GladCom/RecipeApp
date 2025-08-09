using Microsoft.EntityFrameworkCore;
using RecipeApp.Model;

namespace RecipeApp.Services;

/// <summary>
/// DBContext для рецептов.
/// </summary>
/// <param name="options">Настройка контекста.</param>
public class RecipesDbContext(DbContextOptions<RecipesDbContext> options)
  : DbContext(options)
{
  #region Поля и свойства

  /// <summary>
  /// Рецепты.
  /// </summary>
  public DbSet<Recipe> Recipes { get; set; }

  /// <summary>
  /// Ингредиенты.
  /// </summary>
  public DbSet<Ingredient> Ingredients { get; set; }

  /// <summary>
  /// Связи рецептов с ингредиентами.
  /// </summary>
  public DbSet<RecipeIngredient> RecipeIngredients { get; set; }

  #endregion

  #region Базовый класс

  /// <inheritdoc/>
  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<RecipeIngredient>()
      .HasKey(ri => new { ri.RecipeId, ri.IngredientId });

    modelBuilder.Entity<RecipeIngredient>()
      .HasOne(ri => ri.Recipe)
      .WithMany(r => r.RecipeIngredients)
      .HasForeignKey(ri => ri.RecipeId)
      .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<RecipeIngredient>()
      .HasOne(ri => ri.Ingredient)
      .WithMany(i => i.RecipeIngredients)
      .HasForeignKey(ri => ri.IngredientId)
      .OnDelete(DeleteBehavior.Restrict);
  }

  #endregion
}